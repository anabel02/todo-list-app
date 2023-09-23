import { HttpMethod, commandFetch, getSortedCompletedTodos, getSortedNotCompletedTodos } from "../helpers/fetch";
import { Todo } from "../types/type";
import { addAction, completeAction, editAction, removeAction, setTodos } from "./actions";
import { AppDispatch } from "./store";

    export const addTodo = (task: string, createdDateTime: Date ) => {
        return async (dispatch: AppDispatch) => {
            const resp = await commandFetch("", { task, createdDateTime }, HttpMethod.POST);
            const body = await resp.json();
            if (resp.ok) {
                dispatch(addAction({task, createdDateTime, id: body}));
            } else {
                // mantine error
            }
        };
    };

    export const removeTodo = (todo: Todo) => {
        return async (dispatch: AppDispatch) => {
            const { id } = todo;
            const resp = await commandFetch("", { id }, HttpMethod.DELETE);
            if (resp.ok) {
                dispatch(removeAction(todo));
            } else {
                 // mantine error   
            }
        }
    };
        
    export const editTodo = (todo: Todo) => {
        return async (dispatch: AppDispatch) => {
            const { id, task } = todo;
            const resp = await commandFetch("/Edit", { id, task }, HttpMethod.PUT);
            if (resp.ok) {
                dispatch(editAction(todo));
            } else {
                // mantine error
            }
        }
    };

    export const completeTodo = (todo: Todo) => {
        return async (dispatch: AppDispatch) => {
            const { id, task } = todo;
            const resp = await commandFetch("", { id }, HttpMethod.PUT);
            if (resp.ok) {
                dispatch(completeAction(todo));
            } else {
                // mantine error
            }
        }
    };

    export const loadTodos = () => {
        return async (dispatch: AppDispatch) => {
            const respCompleted = await getSortedCompletedTodos;
            const bodyCompleted: Todo[] = await respCompleted.json();

            const respNotCompleted = await getSortedNotCompletedTodos;
            const bodyNotCompleted: Todo[] = await respNotCompleted.json();

            if (respCompleted.ok && respNotCompleted.ok) {
                dispatch(setTodos(bodyCompleted, bodyNotCompleted));
            } else {
            // mantine error
            }
        }
    };