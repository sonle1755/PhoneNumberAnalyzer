import {
  Table, TableHead, TableRow, TableCell, TableBody, Button
} from "@mui/material";
import type { User } from "./types";

interface Props {
  users: User[];
  onEdit: (user: User) => void;
  onDelete: (id: number) => void;
}

export default function UsersTable({ users, onEdit, onDelete }: Props) {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Username</TableCell>
          <TableCell>Email</TableCell>
          <TableCell width={200}>Actions</TableCell>
        </TableRow>
      </TableHead>

      <TableBody>
        {users.map((u) => (
          <TableRow key={u.id}>
            <TableCell>{u.username}</TableCell>
            <TableCell>{u.email}</TableCell>
            <TableCell>
              <Button onClick={() => onEdit(u)}>Edit</Button>
              <Button color="error" onClick={() => onDelete(u.id)}>
                Delete
              </Button>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}
