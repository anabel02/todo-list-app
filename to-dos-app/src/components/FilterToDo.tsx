import { Center,  Select } from '@mantine/core'
import React from 'react'

export const FilterToDo: React.FC<{callback: (value: string | null) => void}> = ({callback}) => {
  return (
    <Center maw={1900} h={150}>
        <Select data={["All todos", "Completed todos", "Not completed todos"]} value="All" onChange={callback} />
    </Center>)
}
