import { Injectable, inject } from '@angular/core';
import { MessageService } from 'primeng/api';

export type NotificationSeverity = 'success' | 'info' | 'warn' | 'error';

export interface Notification {
  severity: NotificationSeverity;
  title: string;
  description?: string;
  life?: number;
}

@Injectable({ providedIn: 'root' })
export class NotificationService {

  private messageService = inject(MessageService);

  show(notification: Notification): void {
    this.messageService.add({
      severity: notification.severity,
      summary: notification.title,
      detail: notification.description,
      life: notification.life,
    });
  }

  success(title: string, description?: string, life = 3000): void {
    this.show({ severity: 'success', title, description, life });
  }

  info(title: string, description?: string, life = 3000): void {
    this.show({ severity: 'info', title, description, life });
  }

  warn(title: string, description?: string): void {
    this.show({ severity: 'warn', title, description });
  }

  error(title: string, description?: string, life?: number): void {
    this.show({ severity: 'error', title, description, life: life ?? 8000 });
  }
}