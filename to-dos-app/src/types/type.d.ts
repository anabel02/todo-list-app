export type Todo = {
    Id: number;
    Task: string;
    CreatedDateTime: string;
    CompletedDateTime?: string;
};

export type TodoState = {
    completedTodos: Todo[],
    notCompletedTodos: Todo[],
    activeTodos: Todo[],
    loading: boolean
};

export type TodoAction = {
    type: ActionType,
    payload: {
        todo?: Todo;
        completedTodos?: Todo[];
        notCompletedTodos?: Todo[];
        activeTodos?: Todo[];
        loading: boolean
    };
};

export type OdataResponse = {
    "@odata.context": string;
    "value": Todo[];
}

export type DispatchType = (args: TodoAction) => TodoAction