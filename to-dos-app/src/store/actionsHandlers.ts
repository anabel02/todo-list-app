import { HttpMethod, commandFetch, getSortedCompletedTodos, getSortedNotCompletedTodos } from "../helpers/fetch";
import { OdataResponse, Todo } from "../types/type";
import { addAction, completeAction, editAction, removeAction, setActiveTodos, setTodos } from "./actions";
import { AppDispatch, RootState } from "./store";
import { changeStateError, fetchError } from "../helpers/error";
import moment from "moment";

export const addTodo = (task: string) => {
    return async (dispatch: AppDispatch) => {
        try {
            const resp = await commandFetch("", { task }, HttpMethod.POST);
            const body : Todo = await resp.json();
            console.log(body);
            if (resp.ok) {
                dispatch(addAction(body));
            } else {
                changeStateError(); 
            }
        } catch {
            fetchError(); 
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
                changeStateError();    
            }
        } catch {
            fetchError();
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
                changeStateError();  
            }
        } catch {
            fetchError();
        }
    }
};

export const completeTodo = (todo: Todo) => {
    return async (dispatch: AppDispatch) => {
        try {
            const { Id } = todo;
            const resp = await commandFetch("", { Id }, HttpMethod.PUT);
            const body : string = await resp.json();
            if (resp.ok) {
                dispatch(completeAction({ ...todo, CompletedDateTime: body }))
            } else {
                
            }
        } catch {
            
        }
    }
};

export const loadTodos = () => {
    return async (dispatch: AppDispatch) => {
        try {
            const respCompleted = await getSortedCompletedTodos();
            const bodyCompleted: OdataResponse = await respCompleted.json();

            const respNotCompleted = await getSortedNotCompletedTodos();
            const bodyNotCompleted: OdataResponse = await respNotCompleted.json();

            if (respCompleted.ok && respNotCompleted.ok) {
                dispatch(setTodos(bodyCompleted.value, bodyNotCompleted.value));
                dispatch(applyFilter(Filter.All));
            } else {
                changeStateError();
            }
        } catch {
            fetchError();
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
                dispatch(setActiveTodos(getState().notCompletedTodos.concat(getState().completedTodos)));
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