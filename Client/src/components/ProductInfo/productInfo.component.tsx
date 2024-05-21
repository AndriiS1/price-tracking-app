import { useEffect, useState } from "react";
import "./productInfo.component.style.css";
import { LineChart } from "@mui/x-charts/LineChart";
import { Link, useNavigate, useParams } from "react-router-dom";
import productService from "../../Services/product.service";
import {
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  Stack,
  TextField,
} from "@mui/material";
import {
  SingleProduct,
  SingleStoreStatistics,
} from "../../Services/Dtos/trackedProduct.response";
import { StoreStatistic } from "../../Services/Dtos/trackedProducts.response";

function ProductInfo() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [product, setProduct] = useState<SingleProduct>();
  const [store, setStore] = useState<SingleStoreStatistics | undefined>();
  const [chartData, setChartData] = useState<{ x: Date; y: number }[]>([]);

  function parseDate(dateString: string): string {
    const date = new Date(dateString);

    const hours = date.getUTCHours().toString().padStart(2, "0");
    const minutes = date.getUTCMinutes().toString().padStart(2, "0");
    const day = date.getUTCDate().toString().padStart(2, "0");
    const month = (date.getUTCMonth() + 1).toString().padStart(2, "0");
    const year = date.getUTCFullYear();

    return `${hours}:${minutes} ${day}.${month}.${year}`;
  }

  useEffect(() => {
    const fetchProducts = async () => {
      if (id) {
        const data = await productService.GetProduct(id);
        setProduct(data);
      } else {
        navigate("/");
      }
    };

    fetchProducts();
  }, []);

  useEffect(() => {
    if (product && store) {
      const selectedStore = product.storeStatistics.find(
        (s) => s.id === store.id
      );
      if (selectedStore) {
        const newChartData = selectedStore.statistic.map((stat) => ({
          x: new Date(stat.date),
          y: stat.price,
        }));
        setChartData(newChartData);
      }
      console.log(chartData);
    }
  }, [product, store]);

  const handleChange = (event: any) => {
    const selectedStore = product?.storeStatistics.find(
      (s) => s.name === event.target.value
    );
    setStore(selectedStore);
  };

  return (
    <div className="chart-wrap">
      <Stack direction="column" spacing={1} sx={{ width: "100%" }}>
        <Stack direction="row" spacing={1}>
          <FormControl size="small" sx={{ minWidth: 150 }}>
            <InputLabel>Магазин</InputLabel>
            <Select
              value={store?.name || ""}
              onChange={handleChange}
              label="Магазин"
            >
              {product?.storeStatistics.map((store) => (
                <MenuItem key={store.id} value={store.name}>
                  {store.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Stack>
        <LineChart
          xAxis={[
            {
              id: "Years",
              scaleType: "time",
              data: chartData.map((point) => point.x),
              valueFormatter: (date) => parseDate(date),
            },
          ]}
          series={[{ data: chartData.map((point) => point.y) }]}
          width={1000}
          height={400}
          grid={{ vertical: true, horizontal: true }}
        />
      </Stack>
      {store && (
        <div>
          {store.statistic.length > 0 && (
            <>
              <span>
                Продукт почав відслідковуватися з: {store.statistic[0].date}
              </span>
              <br />
            </>
          )}
          {store.statistic.length > 0 && (
            <>
              <span>
                Найменша ціна:{" "}
                {Math.min(...store.statistic.map((stat) => stat.price))}
              </span>
              <br />
            </>
          )}
          {store.statistic.length > 1 && (
            <>
              <span>
                Найбільша ціна:{" "}
                {Math.max(...store.statistic.map((stat) => stat.price))}
              </span>
              <br />
            </>
          )}
          {store.statistic.length > 1 && (
            <>
              <span>
                Рекомендація:{" "}
                {Math.max(...store.statistic.map((stat) => stat.price))}
              </span>
              <br />
            </>
          )}
          <br />
        </div>
      )}
    </div>
  );
}

export default ProductInfo;
