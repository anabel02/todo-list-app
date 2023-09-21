import { Todo } from "../types/todo";

    export const addAction = (todo: Todo): Action => ({
        type: ActionType.Add,
        payload: todo
    });
    
    export const deleteAction = (todo: Todo): Action => ({
        type: ActionType.Delete,
        payload: todo
    });
    
    export const updateAction = (todo: Todo): Action => ({
        type: ActionType.Update,
        payload: todo
    });
    
    export const toggleAction = (todo: Todo): Action => ({
        type: ActionType.Toggle,
        payload: todo
    });