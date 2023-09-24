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
    payload: { todo, loading: true }
});

export const removeAction = (todo: Todo): TodoAction => ({
    type: ActionType.Remove,
    payload: { todo, loading: true }
});

export const editAction = (todo: Todo): TodoAction => ({
    type: ActionType.Edit,
    payload: { todo, loading: true }
});

export const completeAction = (todo: Todo): TodoAction => ({
    type: ActionType.Complete,
    payload: { todo, loading: true }
});

export const setTodos = (completedTodos: Todo[], notCompletedTodos: Todo[]): TodoAction => ({
    type: ActionType.SetTodos,
    payload: { completedTodos, notCompletedTodos, loading: true },
});

export const setActiveTodos = (activeTodos: Todo[]): TodoAction => ({
    type: ActionType.SetActiveTodos,
    payload: { activeTodos, loading: true },
});