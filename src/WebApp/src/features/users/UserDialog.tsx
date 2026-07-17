import {
  Dialog, DialogTitle, DialogContent, DialogActions,
  TextField, Button
} from "@mui/material";
import { useEffect, useState } from "react";
import type { User } from "./types";

interface Props {
  open: boolean;
  onClose: () => void;
  onSave: (user: Partial<User>) => void;
  initialData?: User | null;
}

export default function UserDialog({
  open,
  onClose,
  onSave,
  initialData
}: Props) {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");

  useEffect(() => {
    setUsername(initialData?.username || "");
    setEmail(initialData?.email || "");
  }, [initialData]);

  const handleSave = () => {
    onSave({
      id: initialData?.id,
      username,
      email,
    });
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth>
      <DialogTitle>
        {initialData ? "Edit User" : "Create User"}
      </DialogTitle>

      <DialogContent>
        <TextField
          label="Username"
          fullWidth
          margin="normal"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />

        <TextField
          label="Email"
          fullWidth
          margin="normal"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </DialogContent>

      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>
        <Button variant="contained" onClick={handleSave}>
          Save
        </Button>
      </DialogActions>
    </Dialog>
  );
}
