import { useEffect, useState } from "react";
import tokenService from "../../Services/token.service";
import "./trackedProducts.component.style.css";
import productService from "../../Services/product.service";
import { Product } from "../../Services/Dtos/trackedProduct.response";
import {
  Card,
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
      <div className="main-content-wrap">
        <h1>Найпопулярніші пошукові запити</h1>
        <TableContainer
          component={Paper}
          sx={{ minWidth: 650, maxWidth: 950 }}
          className="product-table"
        >
          <Table
            aria-label="collapsible table"
            sx={{ minWidth: 650, maxWidth: 950 }}
          >
            <TableHead>
              <TableRow>
                <TableCell />
                <TableCell align="center">№</TableCell>
                <TableCell align="center">Назва продукту</TableCell>
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
