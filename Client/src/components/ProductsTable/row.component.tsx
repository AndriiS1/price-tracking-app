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
import { Product } from "../../Services/Dtos/trackedProducts.response";
import atb_logo from "../Utils/Atb_logo.png";
import Fozzy_shop_logo from "../Utils/Fozzy_shop_logo.png";
import { Link } from "react-router-dom";

export function Row(props: { row: Product; index: number }) {
  const { row: product } = props;
  const [open, setOpen] = React.useState(false);

  function GetLogoPath(storeName: string): any {
    if (storeName == "Атб") {
      return atb_logo;
    }
    if (storeName == "Fozzy shop") {
      return Fozzy_shop_logo;
    } else {
      return "";
    }
  }

  function parseDate(dateString: string): string {
    const date = new Date(dateString);

    const hours = date.getUTCHours().toString().padStart(2, "0");
    const minutes = date.getUTCMinutes().toString().padStart(2, "0");
    const day = date.getUTCDate().toString().padStart(2, "0");
    const month = (date.getUTCMonth() + 1).toString().padStart(2, "0"); // Months are zero-indexed
    const year = date.getUTCFullYear();

    return `${hours}:${minutes} ${day}.${month}.${year}`;
  }

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
        <TableCell component="th" scope="row" align="center">
          {props.index + 1}
        </TableCell>
        <TableCell component="th" scope="row" align="center">
          <Link to={`/${product.id}`}>{product.name}</Link>
        </TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1 }}>
              <Typography variant="h6" gutterBottom component="div">
                Історія цін
              </Typography>
              <Table size="small" aria-label="purchases">
                <TableHead>
                  <TableRow>
                    <TableCell align="left">Лого</TableCell>
                    <TableCell align="center">Магазин</TableCell>
                    <TableCell align="center">Середня ціна(₴)</TableCell>
                    <TableCell align="center">Останнє оновлення</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {product.storeStatistics.map((statistic) => (
                    <TableRow key={statistic.id}>
                      <TableCell align="left" component="th" scope="row">
                        <img src={GetLogoPath(statistic.name)} height="30px" />
                      </TableCell>
                      <TableCell align="center" component="th" scope="row">
                        {statistic.name}
                      </TableCell>
                      <TableCell align="center" component="th" scope="row">
                        {statistic.lastStatistic.price}
                      </TableCell>
                      <TableCell align="center" component="th" scope="row">
                        {parseDate(statistic.lastStatistic.date)}
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
