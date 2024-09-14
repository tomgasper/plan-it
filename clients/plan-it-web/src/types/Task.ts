export interface Task {
    name: string;
    description: string;
    id: string;
    dueData: Date;
    taskWorkerIds: string[];
}