import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { filter, Subscription } from 'rxjs';
import { RouteChild, routes } from '../../app.routes';
import { FormsModule } from '@angular/forms';

interface Tab {
  label: string;
  path: string;
  icon?: string;
}

@Component({
  selector: 'app-tabs-control',
  imports: [CommonModule, FormsModule],
  templateUrl: './tabs-control.html',
  styleUrl: './tabs-control.css',
})
export class TabsControl implements OnInit, OnDestroy {

  tabs: Tab[] = [];
  activeTab = '';

  private sub!: Subscription;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.sub = this.router.events.pipe(
      filter(e => e instanceof NavigationEnd)
    ).subscribe((e: NavigationEnd) => {
      this.onNavigate(e.urlAfterRedirects);
    });

    // Handle initial load
    this.onNavigate(this.router.url);
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  private onNavigate(url: string) {
    const tab = this.resolveTab(url);
    if (!tab) return;

    this.activeTab = tab.path;

    const exists = this.tabs.some(t => t.path === tab.path);
    if (!exists) this.tabs.push(tab);
  }

  private resolveTab(url: string): Tab | null {
    // Walk all route configs and find the deepest matching titled route
    return this.findTab(routes, url, '') ?? null;
  }

  private findTab(routeList: RouteChild[], url: string, basePath: string): Tab | null {
    for (const route of routeList) {
      if (!route.path || route.path === '') continue;

      const fullPath = basePath ? `${basePath}/${route.path}` : `/${route.path}`;

      if (route.children?.length) {
        const found = this.findTab(route.children, url, fullPath);
        if (found) return found;
      }

      if (route.title && url.startsWith(fullPath)) {
        return {
          label: route.title,
          path: fullPath,
          icon: route.icon
        };
      }
    }
    return null;
  }

  activateTab(tab: Tab) {
    this.router.navigate([tab.path]);
  }

  closeTab(tab: Tab, event: Event) {
    event.stopPropagation();
    const index = this.tabs.indexOf(tab);
    this.tabs.splice(index, 1);
  
    if (tab.path === this.activeTab) {
      if (this.tabs.length === 0) {
        this.router.navigate(['/empty']);
      } else {
        const next = this.tabs[index] ?? this.tabs[index - 1];
        this.router.navigate([next.path]);
      }
    }
  }
}
