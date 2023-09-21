
    export const addAction = (todo: Todo): Action => ({
        type: ActionType.Add,
        payload: { todo }
    });
    
    export const removeTodo = (todo: Todo): Action => ({
        type: ActionType.Delete,
        payload: { todo }
    });
    
    export const updateTodo = (todo: Todo): Action => ({
        type: ActionType.Update,
        payload: { todo }
    });
    
    export const setActiveTodos = (todos: Array<Todo>): Action => ({
        type: ActionType.Toggle,
        payload: { todos }
    });