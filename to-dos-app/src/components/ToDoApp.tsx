import React, { useEffect, useState } from "react";
import SearchBar from "./SearchBar";
import { Center, Container } from "@mantine/core";
import { AddToDo } from "./AddToDo";
import { RootState, useAppDispatch } from "../store/store";
import { Filter, applyFilter, loadTodos } from "../store/actionsCreator";
import { useSelector } from "react-redux";
import { ToDoFilters } from "./ToDoFilters";
import { ToDoList } from "./ToDoList";

export const ToDoApp = () => {
    const dispatch = useAppDispatch();

    useEffect(() => {
        dispatch(loadTodos());
    }, [dispatch]);

    const activeTodos = useSelector((state: RootState) => state.activeTodos);

    const [filter, setFilter] = useState(Filter.All);

    useEffect(() => {
        dispatch(applyFilter(filter));
    }, [dispatch, filter]);

    const handleFilterChange = (value: Filter) => {
        setFilter(value);
    };

    const [searchQuery, setSearchQuery] = useState("");

    return (
        <>
            <SearchBar SearchState={[searchQuery, setSearchQuery]}/>
            <ToDoFilters handleFilterChange={handleFilterChange} />
            <Center maw={1700} h={700}>
                <Container>
                    <ToDoList todos={searchQuery !== "" ? activeTodos.filter(todo => todo.Task.includes(searchQuery)) : activeTodos} />
                    <AddToDo />
                </Container>
            </Center>
        </>
    );
}
