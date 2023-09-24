import { Input } from "@mantine/core";
import { IconSearch } from "@tabler/icons-react";

function SearchBar({ SearchState }: { SearchState: [string, React.Dispatch<React.SetStateAction<string>>] }) {
  const [searchQuery, setSearchQuery] = SearchState;

  const handleSearchInputChange = (event: any) => {
    setSearchQuery(event.target.value);
  };

  return (
        <Input
          placeholder="Search"
          value={searchQuery}
          onChange={handleSearchInputChange}
          radius="xl"
          rightSection={<IconSearch size={14} />}
        />
  );
}

export default SearchBar;