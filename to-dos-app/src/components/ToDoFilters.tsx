import { Center, Flex, Select } from '@mantine/core';
import React, { useState } from 'react';
import SearchBar from './SearchBar';
import { Filter } from '../store/actionsCreator';

export const ToDoFilters = ({handleFilterChange} : {handleFilterChange: (value: Filter) => void}) => {
    return (
        <Flex
        direction={{ base: "column", sm: "row" }}
        gap="sm"
        align="rigth"
    >
        <Center maw={1900} h={150}>
            <Select data={["All", "Completed", "Not completed"]} defaultValue="All" onChange={handleFilterChange} />
        </Center>
    </Flex>
    )
}
