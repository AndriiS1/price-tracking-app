import { useEffect, useState } from "react";
import tokenService from "../../Services/token.service";
import "./trackedProducts.component.style.css";
import productService from "../../Services/product.service";
import { Product } from "../../Services/Dtos/trackedProduct.response";
import {
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@mui/material";
import { Row } from "./row.component";

export default function TrackedProducts() {
  const [page, setPage] = useState<number>(1);
  const size = 20;
  const [products, setProducts] = useState<Product[]>([]);

  useEffect(() => {
    const fetchProducts = async () => {
      const data = await productService.GetTableUrlsData(page, size);
      setProducts(data);
    };

    fetchProducts();
  }, []);

  return (
    <div>
      <div>
        <h1>Найбільш популярні пошукові запити</h1>
        <TableContainer component={Paper}>
          <Table aria-label="collapsible table">
            <TableHead>
              <TableRow>
                <TableCell />
                <TableCell>№</TableCell>
                <TableCell align="right">Назва продукту</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {products.map((product, index) => (
                <Row key={product.productId} row={product} index={index} />
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </div>
    </div>
  );
}
