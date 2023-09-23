import React, { useEffect, useState } from "react";
import { useFetchToDos } from "../hooks/useFetch";
import SearchBar from "./SearchBar";
import { FilterToDo } from "./FilterToDo";
import { Center, Container } from "@mantine/core";
import { ToDoList } from "./ToDoList";
import { AddToDo } from "./AddToDo";
import { Todo, TodoState } from "../types/type";
import { RootState, useAppDispatch } from "../store/store";
import { addTodo, loadTodos } from "../store/actionsCreator";
import { useSelector } from "react-redux";



export const ToDoApp = () => {
    const dispatch = useAppDispatch();
    
    useEffect(() => {
        dispatch(loadTodos());
    }, [dispatch]);

    const completedTodos = useSelector((state: RootState) => state.completedTodos);
    const notCompletedTodos = useSelector((state: RootState) => state.notCompletedTodos);
    const activeTodos = useSelector((state: RootState) => state.activeTodos);


    const [state, setState] = useState([]);
    const todos = useFetchToDos(``);

    const [searchQuery, setSearchQuery] = useState("");

    const a = (args: string | undefined) =>{
     setSearchQuery(args ?? "")
    }

    const b = (args: string | null) =>{
        setSearchQuery(args ?? "")
       }

    const c = (args: string | null) =>{
        setSearchQuery(args ?? "")
    }
   
     return (
        <>
            <FilterToDo callback={b}/>
        <Center maw={1700} h={700}>
            <Container>
                <SearchBar callback={a}/>
                {/* <ToDoList todos={todos}/> */}
                <ToDoList todos={todos}/>
                <AddToDo />
            </Container>
        </Center>
        </>
    );
}
