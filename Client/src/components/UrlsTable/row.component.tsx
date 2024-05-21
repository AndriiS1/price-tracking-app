import {
  Box,
  Collapse,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import React from "react";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import { Product } from "../../Services/Dtos/trackedProduct.response";

export function Row(props: { row: Product; index: number }) {
  const { row: product } = props;
  const [open, setOpen] = React.useState(false);

  return (
    <React.Fragment>
      <TableRow sx={{ "& > *": { borderBottom: "unset" } }}>
        <TableCell>
          <IconButton
            aria-label="expand row"
            size="small"
            onClick={() => setOpen(!open)}
          >
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell component="th" scope="row">
          {props.index + 1}
        </TableCell>
        <TableCell component="th" scope="row">
          {product.name}
        </TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1 }}>
              <Typography variant="h6" gutterBottom component="div">
                History
              </Typography>
              <Table size="small" aria-label="purchases">
                <TableHead>
                  <TableRow>
                    <TableCell align="left">Магазин</TableCell>
                    <TableCell align="center">Середня ціна(₴)</TableCell>
                    <TableCell align="right">Останнє оновлення</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {product.storeStatistics.map((statistic) => (
                    <TableRow key={statistic.storeId}>
                      <TableCell align="left" component="th" scope="row">
                        {statistic.storeName}
                      </TableCell>
                      <TableCell align="center" component="th" scope="row">
                        {statistic.storeLastStatistic.price}
                      </TableCell>
                      <TableCell align="right" component="th" scope="row">
                        {statistic.storeLastStatistic.date}
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}
