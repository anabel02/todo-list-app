import { Input, Flex, Box } from "@mantine/core";
import { IconSearch } from "@tabler/icons-react";

function SearchBar({ SearchState }: { SearchState: [string, React.Dispatch<React.SetStateAction<string>>] }) {
  const [searchQuery, setSearchQuery] = SearchState;

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
          rightSection={<IconSearch size={14} />}
        />
      </Flex>
    </Box>
  );
}

export default SearchBar;