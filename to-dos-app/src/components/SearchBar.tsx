import { useState } from "react";
import { Input, Button, Flex, Box } from "@mantine/core";

function SearchBar({SearchState} : {SearchState: [string, React.Dispatch<React.SetStateAction<string>>]}) {
  const [searchQuery, setSearchQuery] = SearchState;

  const handleSearchInputChange = (event: any) => {
    setSearchQuery(event.target.value);
  };

  const ResetInput = (e: any) =>{
    setSearchQuery("");
  }

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
          <Button size="xs" onClick={ResetInput} radius="xl">Reset</Button>
        </Flex>
    </Box>
  );
}

export default SearchBar;