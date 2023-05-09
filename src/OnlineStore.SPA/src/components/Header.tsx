import { useEffect } from "react";

function Header() {
  useEffect(() => {
    fetch(`${import.meta.env.VITE_API_BASE_URL}/products`)
      .then((response) => response.json())
      .then((json) => console.log(json))
      .catch((error) => console.error(error));
  }, []);

  return <div>Hello World</div>;
}

export default Header;
