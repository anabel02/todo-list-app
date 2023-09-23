import { Button, Text } from "@mantine/core";
import { modals } from "@mantine/modals"

export const confirm = (handleConfirm: () => void, handleCancel: () => void, action: string) => modals.openConfirmModal({
  title: `Please confirm your action: ${action} task`,
  children: (
    <Text size="sm">
      This action is so important that you are required to confirm it. Please click one of these buttons to proceed.
    </Text>
  ),
  labels: { confirm: 'Confirm', cancel: 'Cancel' },
  onCancel: handleCancel,
  onConfirm: handleConfirm,
});

export const validate = (value: string) => {
  if (value.trim().length > 0) {
    return true;
  }
  modals.open({
    title: 'Invalid Task Name',
    children: (
      <>
        <Text>Task name mustn't be null or empty.</Text>
        <Button fullWidth onClick={() => modals.closeAll()} mt="md">
          Understood
        </Button>
      </>
    ),
  });
  return false;
}
