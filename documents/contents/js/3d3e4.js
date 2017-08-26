jQuery(function(a) {
  function b() {
    e && sessionStorage.setItem("wc_cart_created", new Date().getTime());
  }
  function c(a) {
    e && (localStorage.setItem(f, a), sessionStorage.setItem(f, a));
  }
  function d() {
    a.ajax(g);
  }
  if ("undefined" == typeof wc_cart_fragments_params) return !1;
  var e,
    f = wc_cart_fragments_params.ajax_url.toString() + "-wc_cart_hash";
  try {
    (e =
      "sessionStorage" in window &&
      null !==
        window.sessionStorage), window.sessionStorage.setItem("wc", "test"), window.sessionStorage.removeItem("wc"), window.localStorage.setItem("wc", "test"), window.localStorage.removeItem("wc");
  } catch (a) {
    e = !1;
  }
  var g = {
    url: wc_cart_fragments_params.wc_ajax_url
      .toString()
      .replace("%%endpoint%%", "get_refreshed_fragments"),
    type: "POST",
    success: function(d) {
      d &&
        d.fragments &&
        (
          a.each(d.fragments, function(b, c) {
            a(b).replaceWith(c);
          }),
          e &&
            (
              sessionStorage.setItem(
                wc_cart_fragments_params.fragment_name,
                JSON.stringify(d.fragments)
              ),
              c(d.cart_hash),
              d.cart_hash && b()
            ),
          a(document.body).trigger("wc_fragments_refreshed")
        );
    }
  };
  if (e) {
    var h = null,
      i = 864e5;
    a(document.body).on("wc_fragment_refresh updated_wc_div", function() {
      d();
    }), a(document.body).on("added_to_cart", function(a, d, e) {
      var g = sessionStorage.getItem(f);
      (null !== g && void 0 !== g && "" !== g) ||
        b(), sessionStorage.setItem(wc_cart_fragments_params.fragment_name, JSON.stringify(d)), c(e);
    }), a(document.body).on("wc_fragments_refreshed", function() {
      clearTimeout(h), (h = setTimeout(d, i));
    }), a(window).on("storage onstorage", function(a) {
      f === a.originalEvent.key &&
        localStorage.getItem(f) !== sessionStorage.getItem(f) &&
        d();
    }), a(window).on("pageshow", function(b) {
      b.originalEvent.persisted &&
        (
          a(".widget_shopping_cart_content").empty(),
          a(document.body).trigger("wc_fragment_refresh")
        );
    });
    try {
      var j = a.parseJSON(
          sessionStorage.getItem(wc_cart_fragments_params.fragment_name)
        ),
        k = sessionStorage.getItem(f),
        l = Cookies.get("woocommerce_cart_hash"),
        m = sessionStorage.getItem("wc_cart_created");
      if (
        (
          (null !== k && void 0 !== k && "" !== k) || (k = ""),
          (null !== l && void 0 !== l && "" !== l) || (l = ""),
          k && (null === m || void 0 === m || "" === m)
        )
      )
        throw "No cart_created";
      if (m) {
        var n = 1 * m + i,
          o = new Date().getTime();
        if (n < o) throw "Fragment expired";
        h = setTimeout(d, n - o);
      }
      if (!j || !j["div.widget_shopping_cart_content"] || k !== l)
        throw "No fragment";
      a.each(j, function(b, c) {
        a(b).replaceWith(c);
      }), a(document.body).trigger("wc_fragments_loaded");
    } catch (a) {
      d();
    }
  } else d();
  Cookies.get("woocommerce_items_in_cart") > 0
    ? a(".hide_cart_widget_if_empty").closest(".widget_shopping_cart").show()
    : a(".hide_cart_widget_if_empty")
        .closest(".widget_shopping_cart")
        .hide(), a(document.body).on("adding_to_cart", function() {
    a(".hide_cart_widget_if_empty").closest(".widget_shopping_cart").show();
  });
});
