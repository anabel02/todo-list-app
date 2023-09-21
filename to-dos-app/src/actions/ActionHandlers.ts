import { useFetch } from "../helpers/fetch";

    export const handleAdd = (todo: Todo) => {
        return async () => {
            const request = { task: todo.task };
            const resp = await useFetch("todo", request, "POST");
            const body = await resp.json();
    
            if (resp.ok) {
                //reducer
            };
        };
    };

    export const handleDelete = (todo: Todo) => {
        return async () => {
            const request = todo.id;
            const resp = await useFetch("", request, "DELETE");
            const body = await resp.json();

            if (resp.ok) {
                //reducer
            };
        };
    };
        
    export const handleToggle = (todo: Todo) => {
        return async () => {
            const request = todo.id;
            const resp = await useFetch("", request, "DELETE");
            const body = await resp.json();

            if (resp.ok) {
                //reducer
            };
        };
    };

    export const handleUpdate = (todo: Todo) => {
        return async () => {
            const request = todo.id;
            const resp = await useFetch("", request, "DELETE");
            const body = await resp.json();

            if (resp.ok) {
                //reducer
            };
        };
    };

    export const handleFilterByCompleted = (isCompleted: boolean) => {
        return async () => {
        };
    };

    export const handleFilterByName = (name: string) => {
        return async () => {
        };
    };