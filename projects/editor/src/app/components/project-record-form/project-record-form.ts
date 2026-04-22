import { Component, inject, input, output, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { MultiSelectModule } from 'primeng/multiselect';
import { CheckboxModule } from 'primeng/checkbox';
import { ColorPickerModule } from 'primeng/colorpicker';
import { HydratedProjectRecord, ProjectRecordTag } from '../../model/project-record.model';
import { DialogService } from '../../services/persistence/dialog.service';
import { UserTagService } from '../../services/user-tags/user-tag.service';
import { TagSelector } from "../tag-selector/tag-selector";

@Component({
  selector: 'app-project-record-form',
  imports: [
    CommonModule,
    FormsModule,
    ButtonModule,
    InputTextModule,
    TextareaModule,
    SelectModule,
    MultiSelectModule,
    CheckboxModule,
    ColorPickerModule,
    TagSelector
],
  templateUrl: './project-record-form.html',
})
export class ProjectRecordForm implements OnInit {

  readonly project = input<HydratedProjectRecord | null>(null);
  readonly submitted = output<Partial<HydratedProjectRecord>>();
  readonly cancelled = output<void>();

  private tagService = inject(UserTagService);
  private dialog = inject(DialogService);

  availableTags: ProjectRecordTag[] = [];

  // Form fields
  title = '';
  description = '';
  directoryPath = '';
  color = '';
  isFavorite = false;
  selectedTags: ProjectRecordTag[] = [];

  ngOnInit(): void {
    this.tagService.getAll().subscribe(tags => this.availableTags = tags);

    const p = this.project();
    if (p) {
      this.title = p.title;
      this.description = p.description ?? '';
      this.directoryPath = p.directoryPath;
      this.color = p.color ?? '';
      this.isFavorite = p.isFavorite;
      this.selectedTags = p.tags;
    }
  }

  pickFolder(): void {
    this.dialog.pickFolder().subscribe(folder => {
      if (folder) this.directoryPath = folder;
    });
  }

  submit(): void {
    this.submitted.emit({
      title: this.title,
      description: this.description || undefined,
      directoryPath: this.directoryPath,
      color: this.color || undefined,
      isFavorite: this.isFavorite,
      tags: this.selectedTags,
    });
  }

  cancel(): void {
    this.cancelled.emit();
  }
}