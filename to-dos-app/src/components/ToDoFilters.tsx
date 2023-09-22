import { Flex } from '@mantine/core'
import React, { useState } from 'react'
import SearchBar, { Propis } from './SearchBar'
import { FilterToDo } from './FilterToDo';

export const ToDoFilters = (props: Propis) => {

    const [searchQuery, setSearchQuery] = useState("");

    const a = (args: string | undefined) =>{
        setSearchQuery(args ?? "")
    }

    const b = (args: string | null) =>{
        setSearchQuery(args ?? "")
       }

    return (
        <Flex
        direction={{ base: "column", sm: "row" }}
        gap="sm"
        align="rigth"
    >
        <SearchBar callback={a}/>
        <FilterToDo callback={b}/>
    </Flex>
    )
}
