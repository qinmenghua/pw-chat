<?php

// pack - http://ru2.php.net/pack
function mByte($p) { return pack('C', $p & 0xFF); }

function mf($i, $j) { return ($i >> $j) & 0xFF; }
function mBytes($p, $from)
{
        $packed = '';
        for ($i = $from; $i >= 0; $i -= 8)
                $packed .= mByte(mf($p,$i));
        return $packed;
}

function mChar($p) { return mByte(mf($p,0)) . mByte(mf($p,8)); }
function mString($s)
{
        $s=iconv('utf-8','utf-16le',$s);
        $ret = mUInt32(mb_strlen($s));//mUInt32(count($s) * 2);
        $s=array_merge(unpack("n*",$s));
        for ($i = 0;$i < count($s); $i++)
        {
                $ret .= mByte(($s[$i]) >> 8);
                $ret .= mByte($s[$i]);
        }
        return $ret;
}

function mShort($p) { return mBytes($p,8); }
function mInt($p) { return mBytes($p,24); }
function mLong($p) { return mBytes($p,56); }

function mUInt32($p) {
        if ($p < 64)
                return mByte($p);
        if ($p < 16384)
                return mShort($p | 0x8000);
        if ($p < 536870912)
                return mInt($p | 0xC0000000);
        return mInt(-32) . mInt($p);
}
function mSInt32($p) {
        if ($p >= 0)
                return mUInt32($p);
        $t=-$p;
        if ($t < 64)
                return mByte($p | 0x40);
        if ($t < 16384)
                return mShort($p | 0xA000);
        if ($t < 536870912)
                return mInt($p | 0xD0000000);
        return mInt(-16). mInt($p);
}

function mOctets($p) {
        $packed = mUInt32(count($p));
        for ($i = 0; $i < count($p); $i++)
                $packed .= mByte($p[$i]);
        return $packed;
}