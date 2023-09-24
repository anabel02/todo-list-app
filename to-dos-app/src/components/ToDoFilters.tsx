import { Center, Flex, Select } from '@mantine/core';
import { Filter } from '../store/actionsHandlers';

export const ToDoFilters = ({ handleFilterChange }: { handleFilterChange: (value: Filter) => void }) => {
    return (
            <Select data={["All", "Completed", "Not completed"]} 
            defaultValue="All" 
            onChange={handleFilterChange} 
            checkIconPosition="right"
            />
    )
}
