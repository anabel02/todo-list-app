import { useEffect, useState } from "react";
import SearchBar from "./SearchBar";
import { Container, Flex, Select, Stack, Text } from "@mantine/core";
import { AddToDo } from "./AddToDo";
import { RootState, useAppDispatch } from "../store/store";
import { Filter, applyFilter, loadTodos } from "../store/actionsHandlers";
import { useSelector } from "react-redux";
import { ToDoList } from "./ToDoList";

export const ToDoApp = () => {
    const dispatch = useAppDispatch();

    useEffect(() => {
        dispatch(loadTodos());
    }, [dispatch]);

    const activeTodos = useSelector((state: RootState) => state.activeTodos);
    const completedTodos = useSelector((state: RootState) => state.completedTodos);
    const notCompletedTodos = useSelector((state: RootState) => state.notCompletedTodos);
    const loading = useSelector((state: RootState) => state.loading);

    const [filter, setFilter] = useState(Filter.All);

    useEffect(() => {
        if (!loading)
            dispatch(applyFilter(filter));
    }, [filter]);

    const handleFilterChange = (value: Filter) => {
        setFilter(value);
    };

    const [searchQuery, setSearchQuery] = useState("");

    return (
        <>
            {
                !loading &&
                <Stack>
                    <Flex
                        mih={250}
                        gap="md"
                        justify="center"
                        align="center"
                        direction="row"
                        wrap="wrap"
                    >
                        <SearchBar SearchState={[searchQuery, setSearchQuery]} />

                        <Select data={["All", "Completed", "Not completed"]}
                            defaultValue="All"
                            onChange={handleFilterChange}
                            checkIconPosition="right"
                        />
                    </Flex>

                    <AddToDo />

                    <Container>
                        <Text
                            size="xl"
                            fw={1000}
                            c="blue"
                        >
                            Total tasks: {completedTodos.length + notCompletedTodos.length}
                        </Text>
                        <ToDoList todos={searchQuery !== "" ? activeTodos.filter(todo => todo.Task.includes(searchQuery)) : activeTodos} />
                    </Container>
                </Stack>
            }
        </>
    );
}
