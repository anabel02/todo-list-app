import { Button, Modal, Text } from "@mantine/core";
import { modals } from "@mantine/modals";

export const errorModal = (title: string, description: string) => {
    modals.open({
        title: title,
        children: (
          <>
            <Text>{ description }</Text>
            <Button color="red" fullWidth onClick={() => modals.closeAll()} mt="md">
              Close
            </Button>
          </>
        ),});
}

export const fetchError = () => errorModal("Fetch error", "Attempt to fetch has failed"); 

export const changeStateError = () => errorModal("Application error", "Attempt to change application state has failed");