import React, { FormEventHandler, useEffect, useState } from "react";
import SearchBar from "./SearchBar";
import { Center, Container } from "@mantine/core";
import { AddToDo } from "./AddToDo";
import {  AppDispatch, RootState, useAppDispatch } from "../store/store";
import { Filter, applyFilter, loadTodos } from "../store/actionsCreator";
import { useSelector } from "react-redux";
import { ToDoFilters } from "./ToDoFilters";
import { ToDoList } from "./ToDoList";

export const ToDoApp = () => {
    const dispatch = useAppDispatch();
    
    useEffect(() => {
        dispatch(loadTodos());
    }, [dispatch]);

    const completedTodos = useSelector((state: RootState) => state.completedTodos);
    const notCompletedTodos = useSelector((state: RootState) => state.notCompletedTodos);
    const activeTodos = useSelector((state: RootState) => state.activeTodos);

    const [searchQuery, setSearchQuery] = useState("");
    const [filter, setFilter] = useState(Filter.All);

    useEffect(() => {
        dispatch(applyFilter(filter));
      }, [dispatch, filter]);
   
    const handleSearchClick = (e : FormEventHandler<HTMLButtonElement>) => {
        console.log(e);
      };
    
      const handleFilterChange = (value: Filter) => {
        setFilter(value);
      };

     return (
        <>
            <ToDoFilters handleFilterChange={handleFilterChange}/>
        <Center maw={1700} h={700}>
            <Container>
                <ToDoList todos={activeTodos}/>
                <AddToDo />
            </Container>
        </Center>
        </>
    );
}
