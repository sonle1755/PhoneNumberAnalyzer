import { useMemo } from "react";
import { DataGrid } from "@mui/x-data-grid";
import type { GridColDef } from "@mui/x-data-grid";
import { Chip } from "@mui/material";
import type { PatternTemplateDetail } from "@/shared/api/Api";

interface PatternTemplateTableProps {
  templates: PatternTemplateDetail[];
  isLoading: boolean;
  onRowClick: (id: string) => void;
}

export default function PatternTemplateAdminPage({
  templates,
  isLoading,
  onRowClick,
}: PatternTemplateTableProps) {
  const columns: GridColDef<PatternTemplateDetail>[] = useMemo(
    () => [
      { field: "name", headerName: "Name", flex: 1 },
      { field: "description", headerName: "Description", flex: 2 },
      {
        field: "isEnabled",
        headerName: "Status",
        width: 120,
        renderCell: (params) => (
          <Chip
            label={params.value ? "Enabled" : "Disabled"}
            color={params.value ? "success" : "default"}
            size="small"
          />
        ),
      },
      {
        field: "ownerId",
        headerName: "Visibility",
        width: 120,
        renderCell: (params) => (
          <Chip
            label={params.value ? "Private" : "Public"}
            size="small"
            variant="outlined"
          />
        ),
      },
    ],
    [],
  );

  return (
    <DataGrid
      rows={templates}
      columns={columns}
      getRowId={(row) => row.id}
      loading={isLoading}
      autoHeight
      disableRowSelectionOnClick
      onRowClick={(params) => onRowClick(String(params.id))}
    />
  );
}
