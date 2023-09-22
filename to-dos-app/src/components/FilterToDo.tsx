import { Select } from '@mantine/core'
import React from 'react'

export const FilterToDo: React.FC<{callback: (value: string | null) => void}> = ({callback}) => {
  return (
    <Select data={["All todos", "Completed todos", "Not completed todos"]} value="All" onChange={callback} />
  );
}
