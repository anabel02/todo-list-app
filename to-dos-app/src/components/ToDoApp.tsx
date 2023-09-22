import React, { useState } from "react";
import { useFetchToDos } from "../hooks/useFetch";
import SearchBar from "./SearchBar";
import { FilterToDo } from "./FilterToDo";
import { Center, Container } from "@mantine/core";
import { ToDoList } from "./ToDoList";
import { AddToDo } from "./AddToDo";

export const ToDoApp = () => {
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
                <ToDoList todos={todos}/>
                <AddToDo callback={c}/>
            </Container>
        </Center>
        </>
    );
}
