import { useEffect, useState } from "react";
import { queryFetch } from "../helpers/fetch";

export const useFetchToDos = (query : string) => {
    const [state, setState] = useState([]);

    useEffect(() => {
        queryFetch(query).then(resp => resp.json()).then(values => {
            setState(values.value)})
            .catch(console.warn)
        }, 
        []);

    return state;
}