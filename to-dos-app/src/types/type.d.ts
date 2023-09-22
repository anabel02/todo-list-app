    export type Todo = {
        id: number;
        task: string;
        createdDateTime: Date;
        completedDateTime?: Date;
    };
    
    export type TodoState = {
        articles: Todo[]
    };
    
    export type TodoAction = {
        type: ActionType,
        payload: any
    };
  
    export type DispatchType = (args: TodoAction) => TodoAction