import { Button, Text } from "@mantine/core";
import { modals } from "@mantine/modals"

export const confirm = (handleConfirm: () => void, handleCancel: () => void, action: string, color: string) => {
  modals.openConfirmModal({
    title: `${action} task`,
    centered: true,
    closeOnClickOutside: false,
    children: (
      <Text size="sm">
        Are you sure you want to {action.toLowerCase()} this task?
      </Text>
    ),
    confirmProps: { color: color },
    labels: { confirm: `${action} task`, cancel: `Don't ${action.toLowerCase()} task` },
    onCancel: handleCancel,
    onConfirm: handleConfirm,
  });
}

export const validate = (value: string) => {
  if (value.trim().length > 0) {
    return true;
  }
  modals.open({
    title: 'Invalid Task Name',
    children: (
      <>
        <Text>Task name mustn't be empty.</Text>
        <Button color='red' fullWidth onClick={() => modals.closeAll()} mt="md">
          Understood
        </Button>
      </>
    ),
  });
  return false;
}