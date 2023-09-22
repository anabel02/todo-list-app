import { HttpMethod, commandFetch, queryFetch } from "../helpers/fetch";
import { Todo } from "../types/type";
import { AppDispatch } from "./store";

    export const addTodo = (todo: Todo) => {
        return async (dispatch: AppDispatch) => {
            const { task, createdDateTime } = todo;
            const resp = await commandFetch("", { task, createdDateTime }, HttpMethod.POST);
            const body = await resp.json();
            if (resp.ok) {

            }
        }
    }

    export const removeTodo = (todo: Todo) => {
        return async (dispatch: AppDispatch) => {
            const { id } = todo;
            const resp = await commandFetch("", { id }, HttpMethod.DELETE);
            const body = await resp.json();
            if (resp.ok) {

            }
        }
    };
        
    export const toggleTodo = (todo: Todo) => {
        return async (dispatch: AppDispatch) => {
            const { id } = todo;
            const resp = await commandFetch("", { id }, HttpMethod.PUT);
            const body = await resp.json();
            if (resp.ok) {

            }
        }
    };

    export const updateTodo = (todo: Todo) => {
        return async (dispatch: AppDispatch) => {
            const { id, task } = todo;
            const resp = await commandFetch("", { id, task }, HttpMethod.DELETE);
            const body = await resp.json();
            if (resp.ok) {

            }
        }
    };

    export const setTodos = (filter: Filter) => {

    }