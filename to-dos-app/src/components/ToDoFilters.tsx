import { Center, Flex, Select } from '@mantine/core';
import { Filter } from '../store/actionsHandlers';

export const ToDoFilters = ({ handleFilterChange }: { handleFilterChange: (value: Filter) => void }) => {
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
