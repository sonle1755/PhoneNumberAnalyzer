import { useState } from "react";
import { Container, Typography, Button, Stack } from "@mui/material";
import UsersTable from "./UsersTable";
import UserDialog from "./UserDialog";
import type { User } from "./types";

export default function UsersPage() {
  const [users, setUsers] = useState<User[]>([]);
  const [open, setOpen] = useState(false);
  const [selected, setSelected] = useState<User | null>(null);

  const handleCreate = () => {
    setSelected(null);
    setOpen(true);
  };

  const handleEdit = (user: User) => {
    setSelected(user);
    setOpen(true);
  };

  const handleDelete = (id: number) => {
    setUsers((prev) => prev.filter((u) => u.id !== id));
  };

  const handleSave = (data: Partial<User>) => {
    if (data.id) {
      // update
      setUsers((prev) =>
        prev.map((u) => (u.id === data.id ? { ...u, ...data } as User : u))
      );
    } else {
      // create
      const newUser: User = {
        id: Date.now(),
        username: data.username!,
        email: data.email,
      };
      setUsers((prev) => [...prev, newUser]);
    }

    setOpen(false);
  };

  return (
    <Container sx={{ mt: 4 }}>
      <Stack
        direction="row"
        // justifyContent="space-between"
        // alignItems="center"
        mb={2}
      >
        <Typography variant="h4">Users</Typography>

        <Button variant="contained" onClick={handleCreate}>
          Create User
        </Button>
      </Stack>

      <UsersTable
        users={users}
        onEdit={handleEdit}
        onDelete={handleDelete}
      />

      <UserDialog
        open={open}
        onClose={() => setOpen(false)}
        onSave={handleSave}
        initialData={selected}
      />
    </Container>
  );
}
