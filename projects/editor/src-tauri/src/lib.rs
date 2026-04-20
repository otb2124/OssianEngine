use tauri::Manager;
use std::fs;

#[tauri::command]
fn read_config(relative_path: String) -> Result<String, String> {
    let cwd = std::env::current_dir().map_err(|e| e.to_string())?;
    let full_path = cwd.join(&relative_path);
    println!("read_config cwd: {:?}", cwd);
    println!("read_config full_path: {:?}", full_path);
    fs::read_to_string(&full_path).map_err(|e| format!("{}: {}", full_path.display(), e))
}

#[tauri::command]
fn write_config(relative_path: String, content: String) -> Result<(), String> {
    let cwd = std::env::current_dir().map_err(|e| e.to_string())?;
    let full_path = cwd.join(&relative_path);
    println!("write_config cwd: {:?}", cwd);
    println!("write_config full_path: {:?}", full_path);
    fs::write(&full_path, content).map_err(|e| format!("{}: {}", full_path.display(), e))
}

#[tauri::command]
fn read_config_absolute(path: String) -> Result<String, String> {
    println!("read_config_absolute: {:?}", path);
    fs::read_to_string(&path).map_err(|e| format!("{}: {}", path, e))
}

#[tauri::command]
fn write_config_absolute(path: String, content: String) -> Result<(), String> {
    println!("write_config_absolute: {:?}", path);
    fs::write(&path, content).map_err(|e| format!("{}: {}", path, e))
}

#[tauri::command]
fn greet(name: &str) -> String {
    format!("Hello, {}! You've been greeted from Rust!", name)
}

#[tauri::command]
fn reveal_in_explorer(path: String) {
    #[cfg(target_os = "windows")]
    {
        let windows_path = path.replace('/', "\\");
        std::process::Command::new("explorer")
            .arg(format!("/select,{}", windows_path))
            .spawn()
            .ok();
    }

    #[cfg(target_os = "macos")]
    std::process::Command::new("open")
        .args(["-R", &path])
        .spawn()
        .ok();

    #[cfg(target_os = "linux")]
    std::process::Command::new("xdg-open")
        .arg(&path)
        .spawn()
        .ok();
}

#[tauri::command]
fn scan_for_projects(root: String) -> Result<Vec<String>, String> {
    let root_path = std::path::Path::new(&root);
    let mut found: Vec<String> = Vec::new();
    scan_dir(root_path, &mut found, 0);
    Ok(found)
}

fn scan_dir(dir: &std::path::Path, found: &mut Vec<String>, depth: u32) {
    if depth > 4 { return; }
    let config_path = dir.join("config.json");
    if config_path.exists() {
        if let Some(p) = dir.to_str() {
            found.push(p.to_string());
        }
        return; // don't recurse into project folders
    }
    if let Ok(entries) = fs::read_dir(dir) {
        for entry in entries.flatten() {
            let path = entry.path();
            if path.is_dir() {
                scan_dir(&path, found, depth + 1);
            }
        }
    }
}

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    tauri::Builder::default()
        .plugin(tauri_plugin_opener::init())
        .plugin(tauri_plugin_fs::init())
        .plugin(tauri_plugin_dialog::init())
        .invoke_handler(tauri::generate_handler![
            greet,
            reveal_in_explorer,
            read_config,
            write_config,
            read_config_absolute,
            write_config_absolute,
            scan_for_projects
        ])
        .setup(|app| {
            #[cfg(debug_assertions)]
            {
                let window = app.get_webview_window("main").unwrap();
                window.open_devtools();
            }
            Ok(())
        })
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}