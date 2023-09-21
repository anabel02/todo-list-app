export type Todo = {
    id: number;
    task: string;
    createdDateTime: Date;
    completedDateTime?: Date;
};