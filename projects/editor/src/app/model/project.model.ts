export interface HydratedProject extends Omit<Project, 'tags'> {
    tags: ProjectTag[];
}

export interface Project {
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

export interface ProjectTag {
    id: string;
    label: string;
    color?: string;
  }