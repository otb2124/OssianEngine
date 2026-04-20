export interface HydratedProjectRecord extends Omit<ProjectRecord, 'tags'> {
    tags: ProjectRecordTag[];
}

export interface ProjectRecord {
  id: string;
  title: string;
  description?: string;
  directoryPath: string;

  tags: string[];

  createdAt: Date;
  updatedAt: Date;
  lastOpenedAt?: Date;

  isFavorite: boolean;
  color?: string;
}

export interface ProjectRecordTag {
    id: string;
    label: string;
    color?: string;
  }