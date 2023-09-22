import React, { useState } from "react";
import { useFetchToDos } from "../hooks/useFetch";
import SearchBar from "./SearchBar";
import { FilterToDo } from "./FilterToDo";
import { Box, Container, Flex, ListItem, Modal } from "@mantine/core";
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
            <SearchBar callback={a}/>
            <FilterToDo callback={b}/>
            <ToDoList todos={todos}/>
            <AddToDo callback={c}/>
        </>
    );
}
