function hex_md5(a) {
  return binl2hex(core_md5(str2binl(a), a.length * chrsz));
}
function b64_md5(a) {
  return binl2b64(core_md5(str2binl(a), a.length * chrsz));
}
function str_md5(a) {
  return binl2str(core_md5(str2binl(a), a.length * chrsz));
}
function hex_hmac_md5(a, t) {
  return binl2hex(core_hmac_md5(a, t));
}
function b64_hmac_md5(a, t) {
  return binl2b64(core_hmac_md5(a, t));
}
function str_hmac_md5(a, t) {
  return binl2str(core_hmac_md5(a, t));
}
function md5_vm_test() {
  return "900150983cd24fb0d6963f7d28e17f72" == hex_md5("abc");
}
function core_md5(a, t) {
  (a[t >> 5] |= 128 << (t % 32)), (a[((t + 64) >>> 9 << 4) + 14] = t);
  for (
    var r = 1732584193, e = -271733879, i = -1732584194, s = 271733878, o = 0;
    o < a.length;
    o += 16
  ) {
    var n = r,
      c = e,
      h = i,
      l = s;
    (r = md5_ff(r, e, i, s, a[o + 0], 7, -680876936)), (s = md5_ff(
      s,
      r,
      e,
      i,
      a[o + 1],
      12,
      -389564586
    )), (i = md5_ff(i, s, r, e, a[o + 2], 17, 606105819)), (e = md5_ff(
      e,
      i,
      s,
      r,
      a[o + 3],
      22,
      -1044525330
    )), (r = md5_ff(r, e, i, s, a[o + 4], 7, -176418897)), (s = md5_ff(
      s,
      r,
      e,
      i,
      a[o + 5],
      12,
      1200080426
    )), (i = md5_ff(i, s, r, e, a[o + 6], 17, -1473231341)), (e = md5_ff(
      e,
      i,
      s,
      r,
      a[o + 7],
      22,
      -45705983
    )), (r = md5_ff(r, e, i, s, a[o + 8], 7, 1770035416)), (s = md5_ff(
      s,
      r,
      e,
      i,
      a[o + 9],
      12,
      -1958414417
    )), (i = md5_ff(i, s, r, e, a[o + 10], 17, -42063)), (e = md5_ff(
      e,
      i,
      s,
      r,
      a[o + 11],
      22,
      -1990404162
    )), (r = md5_ff(r, e, i, s, a[o + 12], 7, 1804603682)), (s = md5_ff(
      s,
      r,
      e,
      i,
      a[o + 13],
      12,
      -40341101
    )), (i = md5_ff(i, s, r, e, a[o + 14], 17, -1502002290)), (e = md5_ff(
      e,
      i,
      s,
      r,
      a[o + 15],
      22,
      1236535329
    )), (r = md5_gg(r, e, i, s, a[o + 1], 5, -165796510)), (s = md5_gg(
      s,
      r,
      e,
      i,
      a[o + 6],
      9,
      -1069501632
    )), (i = md5_gg(i, s, r, e, a[o + 11], 14, 643717713)), (e = md5_gg(
      e,
      i,
      s,
      r,
      a[o + 0],
      20,
      -373897302
    )), (r = md5_gg(r, e, i, s, a[o + 5], 5, -701558691)), (s = md5_gg(
      s,
      r,
      e,
      i,
      a[o + 10],
      9,
      38016083
    )), (i = md5_gg(i, s, r, e, a[o + 15], 14, -660478335)), (e = md5_gg(
      e,
      i,
      s,
      r,
      a[o + 4],
      20,
      -405537848
    )), (r = md5_gg(r, e, i, s, a[o + 9], 5, 568446438)), (s = md5_gg(
      s,
      r,
      e,
      i,
      a[o + 14],
      9,
      -1019803690
    )), (i = md5_gg(i, s, r, e, a[o + 3], 14, -187363961)), (e = md5_gg(
      e,
      i,
      s,
      r,
      a[o + 8],
      20,
      1163531501
    )), (r = md5_gg(r, e, i, s, a[o + 13], 5, -1444681467)), (s = md5_gg(
      s,
      r,
      e,
      i,
      a[o + 2],
      9,
      -51403784
    )), (i = md5_gg(i, s, r, e, a[o + 7], 14, 1735328473)), (e = md5_gg(
      e,
      i,
      s,
      r,
      a[o + 12],
      20,
      -1926607734
    )), (r = md5_hh(r, e, i, s, a[o + 5], 4, -378558)), (s = md5_hh(
      s,
      r,
      e,
      i,
      a[o + 8],
      11,
      -2022574463
    )), (i = md5_hh(i, s, r, e, a[o + 11], 16, 1839030562)), (e = md5_hh(
      e,
      i,
      s,
      r,
      a[o + 14],
      23,
      -35309556
    )), (r = md5_hh(r, e, i, s, a[o + 1], 4, -1530992060)), (s = md5_hh(
      s,
      r,
      e,
      i,
      a[o + 4],
      11,
      1272893353
    )), (i = md5_hh(i, s, r, e, a[o + 7], 16, -155497632)), (e = md5_hh(
      e,
      i,
      s,
      r,
      a[o + 10],
      23,
      -1094730640
    )), (r = md5_hh(r, e, i, s, a[o + 13], 4, 681279174)), (s = md5_hh(
      s,
      r,
      e,
      i,
      a[o + 0],
      11,
      -358537222
    )), (i = md5_hh(i, s, r, e, a[o + 3], 16, -722521979)), (e = md5_hh(
      e,
      i,
      s,
      r,
      a[o + 6],
      23,
      76029189
    )), (r = md5_hh(r, e, i, s, a[o + 9], 4, -640364487)), (s = md5_hh(
      s,
      r,
      e,
      i,
      a[o + 12],
      11,
      -421815835
    )), (i = md5_hh(i, s, r, e, a[o + 15], 16, 530742520)), (e = md5_hh(
      e,
      i,
      s,
      r,
      a[o + 2],
      23,
      -995338651
    )), (r = md5_ii(r, e, i, s, a[o + 0], 6, -198630844)), (s = md5_ii(
      s,
      r,
      e,
      i,
      a[o + 7],
      10,
      1126891415
    )), (i = md5_ii(i, s, r, e, a[o + 14], 15, -1416354905)), (e = md5_ii(
      e,
      i,
      s,
      r,
      a[o + 5],
      21,
      -57434055
    )), (r = md5_ii(r, e, i, s, a[o + 12], 6, 1700485571)), (s = md5_ii(
      s,
      r,
      e,
      i,
      a[o + 3],
      10,
      -1894986606
    )), (i = md5_ii(i, s, r, e, a[o + 10], 15, -1051523)), (e = md5_ii(
      e,
      i,
      s,
      r,
      a[o + 1],
      21,
      -2054922799
    )), (r = md5_ii(r, e, i, s, a[o + 8], 6, 1873313359)), (s = md5_ii(
      s,
      r,
      e,
      i,
      a[o + 15],
      10,
      -30611744
    )), (i = md5_ii(i, s, r, e, a[o + 6], 15, -1560198380)), (e = md5_ii(
      e,
      i,
      s,
      r,
      a[o + 13],
      21,
      1309151649
    )), (r = md5_ii(r, e, i, s, a[o + 4], 6, -145523070)), (s = md5_ii(
      s,
      r,
      e,
      i,
      a[o + 11],
      10,
      -1120210379
    )), (i = md5_ii(i, s, r, e, a[o + 2], 15, 718787259)), (e = md5_ii(
      e,
      i,
      s,
      r,
      a[o + 9],
      21,
      -343485551
    )), (r = safe_add(r, n)), (e = safe_add(e, c)), (i = safe_add(
      i,
      h
    )), (s = safe_add(s, l));
  }
  return Array(r, e, i, s);
}
function md5_cmn(a, t, r, e, i, s) {
  return safe_add(bit_rol(safe_add(safe_add(t, a), safe_add(e, s)), i), r);
}
function md5_ff(a, t, r, e, i, s, o) {
  return md5_cmn((t & r) | (~t & e), a, t, i, s, o);
}
function md5_gg(a, t, r, e, i, s, o) {
  return md5_cmn((t & e) | (r & ~e), a, t, i, s, o);
}
function md5_hh(a, t, r, e, i, s, o) {
  return md5_cmn(t ^ r ^ e, a, t, i, s, o);
}
function md5_ii(a, t, r, e, i, s, o) {
  return md5_cmn(r ^ (t | ~e), a, t, i, s, o);
}
function core_hmac_md5(a, t) {
  var r = str2binl(a);
  r.length > 16 && (r = core_md5(r, a.length * chrsz));
  for (var e = Array(16), i = Array(16), s = 0; 16 > s; s++)
    (e[s] = 909522486 ^ r[s]), (i[s] = 1549556828 ^ r[s]);
  var o = core_md5(e.concat(str2binl(t)), 512 + t.length * chrsz);
  return core_md5(i.concat(o), 640);
}
function safe_add(a, t) {
  var r = (65535 & a) + (65535 & t),
    e = (a >> 16) + (t >> 16) + (r >> 16);
  return (e << 16) | (65535 & r);
}
function bit_rol(a, t) {
  return (a << t) | (a >>> (32 - t));
}
function str2binl(a) {
  for (
    var t = Array(), r = (1 << chrsz) - 1, e = 0;
    e < a.length * chrsz;
    e += chrsz
  )
    t[e >> 5] |= (a.charCodeAt(e / chrsz) & r) << (e % 32);
  return t;
}
function binl2str(a) {
  for (var t = "", r = (1 << chrsz) - 1, e = 0; e < 32 * a.length; e += chrsz)
    t += String.fromCharCode((a[e >> 5] >>> (e % 32)) & r);
  return t;
}
function binl2hex(a) {
  for (
    var t = hexcase ? "0123456789ABCDEF" : "0123456789abcdef", r = "", e = 0;
    e < 4 * a.length;
    e++
  )
    r +=
      t.charAt((a[e >> 2] >> (e % 4 * 8 + 4)) & 15) +
      t.charAt((a[e >> 2] >> (e % 4 * 8)) & 15);
  return r;
}
function binl2b64(a) {
  for (
    var t = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/",
      r = "",
      e = 0;
    e < 4 * a.length;
    e += 3
  )
    for (
      var i =
          (((a[e >> 2] >> (8 * (e % 4))) & 255) << 16) |
          (((a[(e + 1) >> 2] >> (8 * ((e + 1) % 4))) & 255) << 8) |
          ((a[(e + 2) >> 2] >> (8 * ((e + 2) % 4))) & 255),
        s = 0;
      4 > s;
      s++
    )
      r +=
        8 * e + 6 * s > 32 * a.length
          ? b64pad
          : t.charAt((i >> (6 * (3 - s))) & 63);
  return r;
}
"undefined" == typeof console &&
  (console = { log: function(a) {}, debug: function(a) {} });
var Gravatar = {
    profile_stack: {},
    profile_map: {},
    overTimeout: !1,
    outTimeout: !1,
    stopOver: !1,
    active_grav: !1,
    active_hash: !1,
    active_id: !1,
    active_grav_clone: !1,
    profile_cb: null,
    stats_queue: [],
    throbber: null,
    has_bg: !1,
    disabled: !1,
    url_prefix: "http://en",
    disable: function() {
      (Gravatar.disabled = !0), Gravatar.hide_card();
      var a = new Date(2100, 1, 1, 1, 1, 1);
      Gravatar.stat("disable"), -1 ==
      window.location.host.search(/wordpress.com/i)
        ? (document.cookie = "nohovercard=1; expires=" + a.toUTCString() + ";")
        : (document.cookie =
            "nohovercard=1; expires=" +
            a.toUTCString() +
            "; domain=.wordpress.com; path=/");
    },
    mouseOut: function(a) {
      a.stopImmediatePropagation(), (Gravatar.stopOver = !0), (Gravatar.outTimeout = setTimeout(
        function() {
          Gravatar.hide_card();
        },
        300
      ));
    },
    init: function(a, t) {
      var r,
        e,
        i = document.cookie.split(";");
      for (r = 0; r < i.length; r++) {
        for (e = i[r]; " " == e.charAt(0); ) e = e.substring(1, e.length);
        if (0 == e.indexOf("nohovercard=1")) return;
      }
      "https:" == window.location.protocol &&
        (this.url_prefix = "https://secure"), this.attach_profiles(
        a,
        t
      ), this.add_card_css(), jQuery(
        "body"
      ).on(
        "mouseenter.gravatar mouseleave.gravatar",
        "img.grav-hashed",
        function(a) {
          if (!Gravatar.disabled) {
            if (
              (
                a.preventDefault(),
                a.stopPropagation(),
                "mouseleave" == a.type || "mouseout" == a.type
              )
            )
              return Gravatar.mouseOut.call(this, a);
            (Gravatar.stopOver = !1), (Gravatar.active_id = jQuery(this).attr(
              "id"
            )), (Gravatar.active_hash = Gravatar.active_id.split(
              "-"
            )[1]), Gravatar.untilt_gravatar(), clearTimeout(
              Gravatar.overTimeout
            ), !1 !== Gravatar.profile_map["g" + Gravatar.active_hash] &&
              (
                Gravatar.stat("hover"),
                clearTimeout(Gravatar.outTimeout),
                Gravatar.tilt_gravatar(),
                Gravatar.fetch_profile_by_hash(
                  Gravatar.active_hash,
                  Gravatar.active_id
                ),
                (Gravatar.overTimeout = setTimeout(function() {
                  Gravatar.show_card();
                }, 600))
              );
          }
        }
      ), jQuery(
        "body"
      ).on(
        "mouseenter.gravatar mouseleave.gravatar",
        "div.gcard, img.grav-clone",
        function(a) {
          Gravatar.disabled ||
            (
              a.preventDefault(),
              a.stopPropagation(),
              "mouseenter" == a.type || "mouseover" == a.type
                ? ((Gravatar.stopOver = !1), clearTimeout(Gravatar.outTimeout))
                : Gravatar.mouseOut.call(this, a)
            );
        }
      ), jQuery(window).bind("scroll", function() {
        Gravatar.active_hash.length && Gravatar.hide_card();
      });
    },
    attach_profiles: function(a, t) {
      setInterval(Gravatar.send_stats, 3e3), (a =
        "undefined" == typeof a ? "body" : a), t &&
        "string" == typeof t &&
        jQuery(t).addClass("no-grav"), jQuery(
        a + ' img[src*="gravatar.com/avatar"]'
      )
        .not(".no-grav, .no-grav img")
        .each(function() {
          if (
            (
              (hash = Gravatar.extract_hash(this)),
              (uniq = 0),
              jQuery("#grav-" + hash + "-" + uniq).length
            )
          )
            for (; jQuery("#grav-" + hash + "-" + uniq).length; ) uniq++;
          var a = jQuery(this)
            .attr("id", "grav-" + hash + "-" + uniq)
            .attr("title", "")
            .removeAttr("title");
          a.parent("a").size() &&
            a
              .parent("a")
              .attr("title", "")
              .removeAttr(
                "title"
              ), a.addClass("grav-hashed"), (a.parents("#comments, .comments, #commentlist, .commentlist, .grav-hijack").size() || !a.parents("a:first").size()) && a.addClass("grav-hijack");
        });
    },
    show_card: function() {
      if (!Gravatar.stopOver) {
        if (
          (
            (dom_id = this.profile_map["g" + Gravatar.active_hash]),
            jQuery(".gcard").hide(),
            "fetching" == this.profile_stack["g" + Gravatar.active_hash]
          )
        )
          return Gravatar.show_throbber(), this.listen(
            Gravatar.active_hash,
            "show_card"
          ), void Gravatar.stat("wait");
        if (
          "undefined" == typeof this.profile_stack["g" + Gravatar.active_hash]
        )
          return Gravatar.show_throbber(), this.listen(
            Gravatar.active_hash,
            "show_card"
          ), void this.fetch_profile_by_hash(Gravatar.active_hash, dom_id);
        Gravatar.stat("show"), Gravatar.hide_throbber(), jQuery(
          "#profile-" + this.active_hash
        ).length ||
          this.build_card(
            this.active_hash,
            this.profile_stack["g" + this.active_hash]
          ), this.render_card(this.active_grav, "profile-" + this.active_hash);
      }
    },
    hide_card: function() {
      clearTimeout(Gravatar.overTimeout), this.untilt_gravatar(), jQuery(
        "div.gcard"
      )
        .filter("#profile-" + this.active_hash)
        .fadeOut(120, function() {
          jQuery("img.grav-large").stop().remove();
        })
        .end()
        .not("#profile-" + this.active_hash)
        .hide();
    },
    render_card: function(a, t) {
      var r = jQuery("#" + t).stop(),
        e = a,
        i = e.offset();
      if (null != i) {
        var s = e.width(),
          o = e.height(),
          n = 5 + 0.4 * s,
          c = r.width(),
          h = r.height();
        c == jQuery(window).width() && ((c = 400), (h = 200));
        var l = i.left - 17,
          d = i.top - 7,
          f = "pos-right";
        i.left + s + n + c >
          jQuery(window).width() + jQuery(window).scrollLeft() &&
          ((l = i.left - c + s + 17), (f = "pos-left"));
        var v = 0.25 * o;
        jQuery("#" + t)
          .removeClass("pos-right pos-left")
          .addClass(f)
          .css({ top: d - v + "px", left: l + "px" });
        var u = o / 2;
        u > h && (u = h / 2), u > h / 2 - 6 && (u = h / 2 - 6), u > 53 &&
          (u = 53), this.has_bg && (u -= 8), 0 > u && (u = 0);
        var g = { height: 2 * o + v + "px" };
        "pos-right" == f
          ? (
              (g.right = "auto"),
              (g.left = "-7px"),
              (g["background-position"] = "0px " + u + "px")
            )
          : (
              (g.right = "-10px"),
              (g.left = "auto"),
              (g["background-position"] = "0px " + u + "px")
            ), jQuery("#" + t + " .grav-cardarrow").css(g);
      }
      r
        .stop()
        .css({ opacity: 0 })
        .show()
        .animate({ opacity: 1 }, 150, "linear", function() {
          jQuery(this).css({ opacity: "auto" }), jQuery(this).stop();
        });
    },
    build_card: function(a, t) {
      (Object.size = function(a) {
        var t,
          r = 0;
        for (t in a) a.hasOwnProperty(t) && r++;
        return r;
      }), GProfile.init(t);
      var r = GProfile.get("urls"),
        e = (GProfile.get("photos"), GProfile.get("accounts")),
        i = 100;
      (i += Object.size(r) > 3 ? 90 : 10 + 20 * Object.size(r)), Object.size(
        e
      ) > 0 && (i += 30);
      var s = GProfile.get("aboutMe");
      (s = s.replace(/<[^>]+>/gi, "")), (s = s.toString().substr(0, i)), i ==
        s.length &&
        (s +=
          '<a href="' +
          GProfile.get("profileUrl") +
          '" target="_blank">&#8230;</a>');
      var o = "grav-inner";
      Gravatar.my_hash &&
        a == Gravatar.my_hash &&
        (
          (o += " grav-is-user"),
          s.length ||
            (s =
              "<p>Want a better profile? <a class='grav-edit-profile' href='http://gravatar.com/profiles/edit/?noclose' target='_blank'>Click here</a>.</p>")
        ), s.length && (o += " gcard-about"), (name = GProfile.get(
        "displayName"
      )), name.length || (name = GProfile.get("preferredUsername"));
      var n =
        '<div id="profile-' +
        a +
        '" class="gcard grofile"> 						<div class="grav-inner"> 							<div class="grav-grav"> 								<a href="' +
        GProfile.get("profileUrl") +
        '" target="_blank"> 									<img src="' +
        GProfile.get("thumbnailUrl") +
        '?s=100&r=pg&d=mm" width="100" height="100" /> 								</a> 							</div> 							<div class="grav-info"> 								<h4><a href="' +
        GProfile.get("profileUrl") +
        '" target="_blank">' +
        name +
        '</a></h4> 								<p class="grav-loc">' +
        GProfile.get("currentLocation") +
        '</p> 								<p class="grav-about">' +
        s +
        '</p> 								<div class="grav-view-complete-button"> 									<a href="' +
        GProfile.get("profileUrl") +
        '" target="_blank" class="grav-view-complete">View Complete Profile</a> 								</div> 								<p class="grav-disable"><a href="#" onclick="Gravatar.disable(); return false">Turn off hovercards</a></p> 							</div> 							<div style="clear:both"></div> 						</div> 						<div class="grav-cardarrow"></div> 						<div class="grav-tag"><a href="http://gravatar.com/" title="Powered by Gravatar.com" target="_blank">&nbsp;</a></div> 					</div>';
      jQuery("body").append(jQuery(n)), jQuery(
        "#profile-" + a + " .grav-inner"
      ).addClass(o), (this.has_bg = !1);
      var c = GProfile.get("profileBackground");
      if (Object.size(c)) {
        this.has_bg = !0;
        var h = { padding: "8px 0" };
        c.color && (h["background-color"] = c.color), c.url &&
          (h["background-image"] = "url(" + c.url + ")"), c.position &&
          (h["background-position"] = c.position), c.repeat &&
          (h["background-repeat"] = c.repeat), jQuery("#profile-" + a).css(
          h
        ), jQuery("#profile-" + a + " .grav-tag").css("top", "8px");
      }
      jQuery("#profile-" + a + " .gcard-links").length ||
        jQuery("#profile-" + a + " .gcard-services").length ||
        jQuery("#profile-" + a + " .grav-rightcol").css({
          width: "auto"
        }), jQuery("#profile-" + a + " .gcard-about").length ||
        jQuery("#profile-" + a + " .grav-leftcol").css({
          width: "auto"
        }), jQuery.isFunction(Gravatar.profile_cb) &&
        Gravatar.loaded_js(a, "profile-" + a), jQuery(
        "#profile-" + a + " a.grav-extra-comments"
      ).click(function(a) {
        return Gravatar.stat("click_comment", a);
      }), jQuery("#profile-" + a + " a.grav-extra-likes").click(function(a) {
        return Gravatar.stat("click_like", a);
      }), jQuery("#profile-" + a + " .grav-links a").click(function(a) {
        return Gravatar.stat("click_link", a);
      }), jQuery("#profile-" + a + " .grav-services a").click(function(a) {
        return Gravatar.stat("click_service", a);
      }), jQuery(
        "#profile-" +
          a +
          " h4 a, #profile-" +
          a +
          " .grav-view-complete, #profile-" +
          a +
          " .grav-grav a"
      ).click(function(a) {
        return Gravatar.stat("to_profile", a);
      }), jQuery("#profile-" + a + " .grav-tag a")
        .click(function(a) {
          return 3 == a.which ||
          2 == a.button ||
          a.altKey ||
          a.metaKey ||
          a.ctrlKey
            ? (
                a.preventDefault(),
                a.stopImmediatePropagation(),
                Gravatar.stat("egg"),
                Gravatar.whee()
              )
            : Gravatar.stat("to_gravatar", a);
        })
        .bind("contextmenu", function(a) {
          return a.preventDefault(), a.stopImmediatePropagation(), Gravatar.stat("egg"), Gravatar.whee();
        }), jQuery("#profile-" + a + " a.grav-edit-profile").click(function(a) {
        return Gravatar.stat("click_edit_profile", a);
      });
    },
    tilt_gravatar: function() {
      if (
        (
          (this.active_grav = jQuery("img#" + this.active_id)),
          !jQuery("img#grav-clone-" + this.active_hash).length
        )
      ) {
        this.active_grav_clone = this.active_grav
          .clone()
          .attr("id", "grav-clone-" + this.active_hash)
          .addClass("grav-clone");
        var a =
            this.active_grav.offset().top +
            parseInt(this.active_grav.css("padding-top"), 10),
          t =
            this.active_grav.offset().left +
            parseInt(this.active_grav.css("padding-left"), 10),
          r = {
            "-webkit-box-shadow": "0 0 4px rgba(0,0,0,.4)",
            "-moz-box-shadow": "0 0 4px rgba(0,0,0,.4)",
            "box-shadow": "0 0 4px rgba(0,0,0,.4)",
            "border-width":
              "2px 2px " + this.active_grav.height() / 5 + "px 2px",
            "border-color": "#fff",
            "border-style": "solid",
            padding: "0px",
            margin: "-2px 0 0 -2px"
          };
        if (this.active_grav.hasClass("grav-hijack"))
          var e =
            '<a href="http://gravatar.com/' +
            this.active_hash +
            '" class="grav-clone-a" target="_blank"></a>';
        else var e = this.active_grav.parents("a:first").clone(!0).empty();
        var i = this.active_grav_clone
          .css(r)
          .wrap(e)
          .parent()
          .css({
            position: "absolute",
            top: a + "px",
            left: t + "px",
            "z-index": 15,
            border: "none",
            "text-decoration": "none"
          });
        jQuery("body").append(i), this.active_grav_clone.removeClass(
          "grav-hashed"
        );
      }
    },
    untilt_gravatar: function() {
      jQuery(
        "img.grav-clone, a.grav-clone-a"
      ).remove(), Gravatar.hide_throbber();
    },
    show_throbber: function() {
      Gravatar.throbber ||
        (Gravatar.throbber = jQuery(
          '<div id="grav-throbber" style="position: absolute; z-index: 16"><img src="' +
            this.url_prefix +
            '.gravatar.com/images/throbber.gif" alt="." width="15" height="15" /></div>'
        )), jQuery("body").append(Gravatar.throbber);
      var a = jQuery("#" + Gravatar.active_id).offset();
      Gravatar.throbber.css({ top: a.top + 2 + "px", left: a.left + 1 + "px" });
    },
    hide_throbber: function() {
      Gravatar.throbber && Gravatar.throbber.remove();
    },
    fetch_profile_by_email: function(a) {
      return this.fetch_profile_by_hash(this.md5(a.toString().toLowerCase()));
    },
    fetch_profile_by_hash: function(a, t) {
      return (this.profile_map["g" + a] = t), this.profile_stack["g" + a] &&
      "object" == typeof this.profile_stack["g" + a]
        ? this.profile_stack["g" + a]
        : (
            (this.profile_stack["g" + a] = "fetching"),
            Gravatar.stat("fetch"),
            void this.load_js(
              this.url_prefix +
                ".gravatar.com/" +
                a +
                ".json?callback=Gravatar.fetch_profile_callback",
              function() {
                Gravatar.fetch_profile_error(a, t);
              }
            )
          );
    },
    fetch_profile_callback: function(a) {
      a &&
        "object" == typeof a &&
        (
          (this.profile_stack["g" + a.entry[0].hash] = a),
          this.notify(a.entry[0].hash)
        );
    },
    fetch_profile_error: function(a, t) {
      Gravatar.stat("profile_404"), (Gravatar.profile_map["g" + a] = !1);
      var r = jQuery("#" + t);
      r.parent('a[href="http://gravatar.com/' + a + '"]').size() &&
        r.unwrap(), t == Gravatar.active_id && Gravatar.hide_card();
    },
    listen: function(t, r) {
      for (
        this.notify_stack || (this.notify_stack = {}), t = "g" + t, this
          .notify_stack[t] || (this.notify_stack[t] = []), a = 0;
        a < this.notify_stack[t].length;
        a++
      )
        if (r == this.notify_stack[t][a]) return;
      this.notify_stack[t][this.notify_stack[t].length] = r;
    },
    notify: function(t) {
      for (
        this.notify_stack || (this.notify_stack = {}), t = "g" + t, this
          .notify_stack[t] || (this.notify_stack[t] = []), a = 0;
        a < this.notify_stack[t].length;
        a++
      )
        0 != this.notify_stack[t][a] &&
          "undefined" != typeof this.notify_stack[t][a] &&
          (
            Gravatar[this.notify_stack[t][a]](t.substr(1)),
            (this.notify_stack[t][a] = !1)
          );
    },
    extract_hash: function(a) {
      if (
        (
          (hash = /gravatar.com\/avatar\/([0-9a-f]{32})/.exec(
            jQuery(a).attr("src")
          )),
          null != hash && "object" == typeof hash && 2 == hash.length
        )
      )
        hash = hash[1];
      else {
        if (
          (
            (hash = /gravatar_id\=([0-9a-f]{32})/.exec(jQuery(a).attr("src"))),
            null === hash || "object" != typeof hash || 2 != hash.length
          )
        )
          return !1;
        hash = hash[1];
      }
      return hash;
    },
    load_js: function(a, t) {
      if (
        (
          this.loaded_scripts || (this.loaded_scripts = []),
          !this.loaded_scripts[a]
        )
      ) {
        this.loaded_scripts[a] = !0;
        var r = document.createElement("script");
        (r.src = a), (r.type = "text/javascript"), jQuery.isFunction(t) &&
          (r.onerror = t), document
          .getElementsByTagName("head")[0]
          .appendChild(r);
      }
    },
    loaded_js: function(a, t) {
      Gravatar.profile_cb(a, t);
    },
    add_card_css: function() {
      if (!jQuery("#gravatar-card-css").length) {
        var a,
          t = jQuery('script[src*="/js/gprofiles."]').attr("src") || !1,
          r = !1;
        if (
          (
            t
              ? (
                  (a = t.replace(/\/js\/gprofiles(?:\.dev)?\.js.*$/, "")),
                  (r = t.split("?")[1] || !1)
                )
              : (a = "//s.gravatar.com"),
            !r
          )
        )
          var e = new Date(),
            i = new Date(e.getFullYear(), 0, 1),
            r = Math.ceil(((e - i) / 864e5 + i.getDay() + 1) / 7),
            r = "ver=" + e.getFullYear().toString() + r.toString();
        (a = a.replace(/^(https?\:)?\/\//, "")), (a =
          window.location.protocol + "//" + a), (new_css =
          "<link rel='stylesheet' type='text/css' id='gravatar-card-css' href='" +
          a +
          "/css/hovercard.css?" +
          r +
          "' />"), jQuery("#gravatar-card-services-css").length ||
          (new_css +=
            "<link rel='stylesheet' type='text/css' id='gravatar-card-services-css' href='" +
            a +
            "/css/services.css?" +
            r +
            "' />"), jQuery("head").append(new_css);
      }
    },
    md5: function(a) {
      return hex_md5(a);
    },
    autofill: function(a, t) {
      a.length &&
        -1 != a.indexOf("@") &&
        (
          (this.autofill_map = t),
          (hash = this.md5(a.toString().toLowerCase())),
          "undefined" == typeof this.profile_stack["g" + hash]
            ? (
                this.listen(hash, "autofill_data"),
                this.fetch_profile_by_hash(hash)
              )
            : this.autofill_data(hash)
        );
    },
    autofill_data: function(a) {
      GProfile.init(this.profile_stack["g" + a]);
      for (var t in this.autofill_map)
        switch (t) {
          case "url":
            (link = GProfile.get("urls")), (url =
              "undefined" != typeof link[0]
                ? link[0].value
                : GProfile.get("profileUrl")), jQuery(
              "#" + this.autofill_map[t]
            ).val(url);
            break;
          case "urls":
            for (
              links = GProfile.get("urls"), links_str = "", l = 0;
              l < links.length;
              l++
            )
              links_str += links[l].value + "\n";
            jQuery("#" + this.autofill_map[t]).val(links_str);
            break;
          default:
            if (((parts = t.split(/\./)), parts[1])) {
              switch (((val = GProfile.get(t)), parts[0])) {
                case "ims":
                case "phoneNumbers":
                  val = val.value;
                  break;
                case "emails":
                  val = val[0].value;
                case "accounts":
                  val = val.url;
              }
              jQuery("#" + this.autofill_map[t]).val(val);
            } else jQuery("#" + this.autofill_map[t]).val(GProfile.get(t));
        }
    },
    whee: function() {
      if (!Gravatar.whee.didWhee) {
        (Gravatar.whee.didWhee = !0), document.styleSheets[0].addRule
          ? document.styleSheets[0].addRule(
              ".grav-tag a",
              "background-position: 22px 100% !important"
            )
          : jQuery(".grav-tag a").css(
              "background-position",
              "22px 100%"
            ), jQuery('img[src*="gravatar.com/"]')
          .addClass("grav-whee")
          .css({
            "-webkit-box-shadow": "1px 1px 3px #aaa",
            "-moz-box-shadow": "1px 1px 3px #aaa",
            "box-shadow": "1px 1px 3px #aaa",
            border: "2px white solid"
          });
        var a = 0;
        return setInterval(function() {
          jQuery(".grav-whee").css({
            "-webkit-transform": "rotate(-" + a + "deg) scale(1.3)",
            "-moz-transform": "rotate(-" + a + "deg) scale(1.3)",
            transform: "rotate(-" + a + "deg) scale(1.3)"
          }), a++, 360 == a && (a = 0);
        }, 6), !1;
      }
    },
    stat: function(a, t) {
      if ((Gravatar.stats_queue.push(a), t)) {
        var r = t.metaKey || "_blank" == jQuery(t.currentTarget).attr("target");
        return Gravatar.send_stats(function() {
          r || (document.location = t.currentTarget.href);
        }), r;
      }
      Gravatar.stats_queue.length > 10 && Gravatar.send_stats();
    },
    send_stats: function(a) {
      if (document.images) {
        var t = Gravatar.stats_queue;
        if (t.length) {
          var r = new Date();
          Gravatar.stats_queue = [];
          var e =
              "https://pixel.wp.com/g.gif?v=wpcom2&x_grav-hover=" +
              t.join(",") +
              "&rand=" +
              Math.random().toString() +
              "-" +
              r.getTime(),
            i = new Image(1, 1);
          jQuery.isFunction(a) && (i.onload = a), (i.src = e);
        }
      }
    }
  },
  GProfile = {
    data: {},
    init: function(a) {
      return "fetching" == a
        ? !1
        : "undefined" == typeof a.entry[0]
          ? !1
          : void (GProfile.data = a.entry[0]);
    },
    get: function(a) {
      if (-1 != a.indexOf(".")) {
        if (((parts = a.split(/\./)), GProfile.data[parts[0]])) {
          if (GProfile.data[parts[0]][parts[1]])
            return GProfile.data[parts[0]][parts[1]];
          for (i = 0, s = GProfile.data[parts[0]].length; i < s; i++)
            if (
              (GProfile.data[parts[0]][i].type &&
                parts[1] == GProfile.data[parts[0]][i].type) ||
              (GProfile.data[parts[0]][i].shortname &&
                parts[1] == GProfile.data[parts[0]][i].shortname) ||
              (GProfile.data[parts[0]][i].primary && "primary" == parts[1])
            )
              return GProfile.data[parts[0]][i];
        }
        return "";
      }
      return GProfile.data[a]
        ? GProfile.data[a]
        : "url" == a && GProfile.data.urls.length
          ? GProfile.data.urls[0].value
          : "";
    }
  },
  hexcase = 0,
  b64pad = "",
  chrsz = 8;
