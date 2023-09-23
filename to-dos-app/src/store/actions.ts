import { Todo, TodoAction } from "../types/type";

    export enum ActionType {
        Add,
        Remove,
        Complete,
        Edit,
        SetActiveTodos,
        SetTodos
    };

    export const addAction = (todo: Todo): TodoAction => ({
        type: ActionType.Add,
        payload: {todo}
    });
    
    export const removeAction = (todo: Todo): TodoAction => ({
        type: ActionType.Remove,
        payload: {todo}
    });
    
    export const editAction = (todo: Todo): TodoAction => ({
        type: ActionType.Edit,
        payload: {todo}
    });
    
    export const completeAction = (todo: Todo): TodoAction => ({
        type: ActionType.Complete,
        payload: {todo}
    });

    export const setTodos = (completedTodos: Todo[], notCompletedTodos: Todo[]): TodoAction => ({
        type: ActionType.SetTodos,
        payload: { completedTodos, notCompletedTodos },
    });

    export const setActiveTodos = (activeTodos: Todo[]): TodoAction => ({
        type: ActionType.SetActiveTodos,
        payload: { activeTodos },
    });