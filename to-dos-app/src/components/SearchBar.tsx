import { FormEventHandler, useState } from "react";
import { Input, Button, Flex, Box } from "@mantine/core";

export interface Propis{
  callback: (arg:string | undefined) => void
}

function SearchBar({handleSearchClick} : {handleSearchClick: FormEventHandler<HTMLButtonElement>}) {
  const [searchQuery, setSearchQuery] = useState("");

  const handleSearchInputChange = (event: any) => {
    setSearchQuery(event.target.value);
  };

  return (
      <Box maw={340} h={70} mx="auto">
        <Flex
          direction={{ base: "column", sm: "row" }}
          gap="sm"
          align="rigth"
        >
          <Input
            placeholder="Search"
            value={searchQuery}
            onChange={handleSearchInputChange}
            radius="xl"
          />
          <Button onSubmit={handleSearchClick} size="xs" radius="xl">Search</Button>
        </Flex>
    </Box>
  );
}

export default SearchBar;