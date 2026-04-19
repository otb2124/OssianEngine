import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute, RouterLink, ROUTES, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { filter } from 'rxjs/operators';

interface SiblingItem {
  label: string;
  routerLink: string;
}

interface BreadcrumbItem {
  label: string;
  routerLink: string;
  siblings: SiblingItem[];
}

@Component({
  selector: 'app-url-breadcrumb',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './url-breadcrumb.html',
  styleUrl: './url-breadcrumb.css'
})
export class UrlBreadcrumbComponent implements OnInit {

  items: BreadcrumbItem[] = [];
  openIndex: number | null = null;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject(ROUTES) private routeConfig: Routes[]
  ) {}

  ngOnInit() {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.buildBreadcrumb();
      this.openIndex = null;
    });
    this.buildBreadcrumb();
  }

  @HostListener('document:click')
  onDocumentClick() {
    this.openIndex = null;
  }

  toggleSiblings(event: Event, index: number) {
    event.stopPropagation();
    this.openIndex = this.openIndex === index ? null : index;
  }

  selectSibling(sib: SiblingItem) {
    this.openIndex = null;
    this.router.navigate([sib.routerLink]);
  }

  private buildBreadcrumb() {
    this.items = [];

    // Flatten ROUTES (it's a Routes[][] from multi-provider token)
    const flatRoutes: Routes = this.routeConfig.flat();

    let currentRoute = this.activatedRoute.root;
    let urlSegments: string[] = [];
    // Track the static config node in parallel so we can find siblings
    let configLevel: Routes = flatRoutes;

    while (currentRoute.children.length > 0) {
      const child = currentRoute.children[0];
      const snap = child.snapshot;
      const label = snap.title;
      const segPath = snap.url.map(s => s.path).join('/');

      // Find the matching config node at this level
      const configNode = configLevel.find(r => r.path === segPath);

      if (label && segPath) {
        urlSegments.push(segPath);
        const routerLink = '/' + urlSegments.join('/');

        // Siblings = all named, titled routes at the same config level
        const siblings: SiblingItem[] = configLevel
          .filter(r => r.path && r.path !== '' && (r as any).title)
          .map(r => {
            // Build sibling path by replacing the last segment
            const sibSegments = [...urlSegments.slice(0, -1), r.path!];
            return {
              label: (r as any).title as string,
              routerLink: '/' + sibSegments.join('/')
            };
          });

        this.items.push({ label, routerLink, siblings });
      }

      // Descend into children for next iteration
      configLevel = configNode?.children ?? [];
      currentRoute = child;
    }
  }
}