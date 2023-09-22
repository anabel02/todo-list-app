import { Todo, TodoAction } from "../types/type";

    export enum ActionType {
        Add,
        Delete,
        Toggle,
        Update,
        SetTodos
    };

    export const addAction = (todo: Todo): TodoAction => ({
        type: ActionType.Add,
        payload: todo
    });
    
    export const deleteAction = (todo: Todo): TodoAction => ({
        type: ActionType.Delete,
        payload: todo
    });
    
    export const updateAction = (todo: Todo): TodoAction => ({
        type: ActionType.Update,
        payload: todo
    });
    
    export const toggleAction = (todo: Todo): TodoAction => ({
        type: ActionType.Toggle,
        payload: todo
    });

    export const setAction = (todos: Todo[]): TodoAction => ({
        type: ActionType.Toggle,
        payload: todos
    });