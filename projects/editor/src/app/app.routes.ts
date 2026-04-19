import { Route} from "@angular/router";

export interface RouteChild extends Route
{
    display?: boolean;
    title?: string;
    icon?: string;
    children?: RouteChild[]
}

export const routes: RouteChild[] =
[
    {
        path: '',
        redirectTo: 'general',
        pathMatch: 'full',
    },
    {
        path: 'general',
        title: 'General',
        icon: 'pi pi-home',
        display: true,
        loadComponent: () => import('./modules/general/general').then(m => m.General),
        children:
        [
            {
                path: '',
                redirectTo: 'home',
                pathMatch: 'full'
            },
            {
                path: 'home',
                title: 'Home',
                icon: 'pi pi-home',
                display: true,
                loadComponent: () => import('./modules/general/pages/home/home').then(m => m.Home),
            },
            {
                path: 'settings',
                title: 'Settings',
                icon: 'pi pi-cog',
                display: true,
                loadComponent: () => import('./modules/general/pages/settings/settings').then(m => m.Settings),
            }
        ]
    }
]