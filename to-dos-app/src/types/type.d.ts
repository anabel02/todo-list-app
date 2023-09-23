    export type Todo = {
        id: number;
        task: string;
        createdDateTime: Date;
        completedDateTime?: Date;
    };
    
    export type TodoState = { 
        completedTodos: Todo[], 
        notCompletedTodos: Todo[],
        activeTodos: Todo[]
    };
    
    export type TodoAction = {
        type: ActionType,
        payload: { 
            todo?: Todo; 
            completedTodos?: Todo[];
            notCompletedTodos?: Todo[];
            activeTodos?: Todo[]
        };
    };
  
    export type DispatchType = (args: TodoAction) => TodoAction