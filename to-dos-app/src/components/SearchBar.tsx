import { useState } from "react";
import { Input, Button, Container, Flex } from "@mantine/core";

interface Propis{
  callback: (arg:string | undefined) => void
}

function SearchBar(props: Propis) {
  const [searchQuery, setSearchQuery] = useState("");
  const {callback} = props;

  const handleSearchInputChange = (event: any) => {
    setSearchQuery(event.target.value);
  };

  const handleSearchClick = (event: any) => {
    event.preventDefault();
    callback(event.target.value);
  };

  return (
    <>
      <Container  mt={120}>
        <Flex
          direction={{ base: "column", sm: "row" }}
          gap="sm"
          align="center"
        >
          <Input
            placeholder="Search"
            value={searchQuery}
            onChange={handleSearchInputChange}
            radius="xl"
          />
        </Flex>
        <Button onClick={handleSearchClick} size="xs" radius="xl">Search</Button>
      </Container>
    </>
  );
};

export default SearchBar;