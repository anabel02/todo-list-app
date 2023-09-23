import { modals } from "@mantine/modals";
import { HttpMethod, commandFetch, getSortedCompletedTodos, getSortedNotCompletedTodos } from "../helpers/fetch";
import { OdataResponse, Todo } from "../types/type";
import { addAction, completeAction, editAction, removeAction, setActiveTodos, setTodos } from "./actions";
import { AppDispatch, RootState } from "./store";

    export const addTodo = (task: string, createdDateTime: Date ) => {
        return async (dispatch: AppDispatch) => {
            try {
                const resp = await commandFetch("", { task, createdDateTime }, HttpMethod.POST);
                const body = await resp.json();
                if (resp.ok) {
                    dispatch(addAction({Task: task, CreatedDateTime: createdDateTime, Id: body}));
                } else {
                    
                }
            } catch {
                
            }
        };
    };

    export const removeTodo = (todo: Todo) => {
        return async (dispatch: AppDispatch) => {
            try {
                const { Id } = todo;
                const resp = await commandFetch("", { Id }, HttpMethod.DELETE);
                if (resp.ok) {
                    dispatch(removeAction(todo));
                } else {
                    // mantine error   
                }
            } catch {

            }
        }
    };
        
    export const editTodo = (todo: Todo) => {
        return async (dispatch: AppDispatch) => {
            try {
                const { Id, Task } = todo;
                const resp = await commandFetch("/Edit", { Id, Task }, HttpMethod.PUT);
                if (resp.ok) {
                    dispatch(editAction(todo));
                } else {
                    // mantine error
                }
            } catch {

            }
        }
    };

    export const completeTodo = (todo: Todo) => {
        return async (dispatch: AppDispatch) => {
            try {
            const { Id } = todo;
                const resp = await commandFetch("", { Id }, HttpMethod.PUT);
                if (resp.ok) {
                    dispatch(completeAction(todo));
                } else {
                    // mantine error
                }
            } catch {

            }
        }
    };

    export const loadTodos = () => {
        return async (dispatch: AppDispatch) => {
            try {
                const respCompleted = await getSortedCompletedTodos;
                const bodyCompleted: OdataResponse = await respCompleted.json();

                const respNotCompleted = await getSortedNotCompletedTodos;
                const bodyNotCompleted: OdataResponse = await respNotCompleted.json();

                if (respCompleted.ok && respNotCompleted.ok) {
                    dispatch(setTodos(bodyCompleted.value, bodyNotCompleted.value));
                    dispatch(setActiveTodos(bodyCompleted.value.concat(bodyNotCompleted.value)));
                } else {
                // mantine error
                }
            } catch {
            }
        }
    };

    export enum Filter {
        All = "All",
        Completed = "Completed",
        NotCompleted = "Not completed"
    }
    
    export const applyFilter = (filter: Filter) => {
        return (dispatch: AppDispatch, getState: () => RootState) => {
            switch (filter) {
                case Filter.All:
                    dispatch(setActiveTodos(getState().completedTodos.concat(getState().notCompletedTodos)));
                    return;
        
                case Filter.Completed:
                    dispatch(setActiveTodos(getState().completedTodos));
                    return;
        
                case Filter.NotCompleted:
                    dispatch(setActiveTodos(getState().notCompletedTodos));
                    return;
            }
        }
    }