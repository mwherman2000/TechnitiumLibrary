/*
Technitium Library
Copyright (C) 2019  Shreyas Zare (shreyas@technitium.com)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TechnitiumLibrary.IO;

namespace TechnitiumLibrary.Net.Dns.ResourceRecords
{
    public class DnsDIDSUBSIGRecord : DnsResourceRecordData
    {
        #region variables

        string _didsubsigTag; // optional primary key
        string _didsubsigDID; // optional secondary key
        string _didsubsigSigData; // "value" field

        #endregion

        #region constructor

        public DnsDIDSUBSIGRecord(string value)
        {
            _didsubsigTag = "";
            _didsubsigDID = "";
            _didsubsigSigData = value;
        }

        public DnsDIDSUBSIGRecord(string didsubsigTag, string didsubsigDID, string didsubsigData)
        {
            _didsubsigTag = didsubsigTag;
            _didsubsigDID = didsubsigDID;
            _didsubsigSigData = didsubsigData;
        }

        public DnsDIDSUBSIGRecord(Stream s)
            : base(s)
        { }

        public DnsDIDSUBSIGRecord(dynamic jsonResourceRecord)
        {
            _length = Convert.ToUInt16(jsonResourceRecord.data.Value.Length);

            string[] parts = (jsonResourceRecord.data.Value as string).Split(' ');

            _didsubsigTag = parts[0];
            _didsubsigDID = parts[1];
            _didsubsigSigData = parts[2];
        }

        #endregion

        #region protected

        protected override void Parse(Stream s)
        {
            int len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didsubsigTag = "";
            if (len > 0) _didsubsigTag = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didsubsigDID = "";
            if (len > 0) _didsubsigDID = Encoding.ASCII.GetString(s.ReadBytes(len));

            len = s.ReadByte();
            if (len < 0)
                throw new EndOfStreamException();
            _didsubsigSigData = "";
            if (len > 0) _didsubsigSigData = Encoding.ASCII.GetString(s.ReadBytes(len));
        }

        protected override void WriteRecordData(Stream s, List<DnsDomainOffset> domainEntries)
        {
            s.WriteByte(Convert.ToByte(_didsubsigTag.Length));
            if (_didsubsigTag.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didsubsigTag));

            s.WriteByte(Convert.ToByte(_didsubsigDID.Length));
            if (_didsubsigDID.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didsubsigDID));

            s.WriteByte(Convert.ToByte(_didsubsigSigData.Length));
            if (_didsubsigSigData.Length > 0) s.Write(Encoding.ASCII.GetBytes(_didsubsigSigData));
        }

        #endregion

        #region public

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            DnsDIDSUBSIGRecord other = obj as DnsDIDSUBSIGRecord;
            if (other == null)
                return false;

            if (this._didsubsigTag.Length > 0)
            {
                if (!this._didsubsigTag.Equals(other._didsubsigTag, StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            else if (this._didsubsigDID.Length > 0)
            {
                if (this._didsubsigDID != other._didsubsigDID)
                    return false;
            }
            else
            {
                if (this._didsubsigSigData != other._didsubsigSigData)
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _didsubsigSigData.GetHashCode();
        }

        public override string ToString()
        {
            return DnsDatagram.EncodeCharacterString(_didsubsigTag + ":" + _didsubsigDID + "=" + _didsubsigSigData );
        }

        #endregion

        #region properties

        public string Tag
        { get { return _didsubsigTag; } }
        public string DID
        { get { return _didsubsigDID; } }
        public string SigData
        { get { return _didsubsigSigData; } }

        #endregion
    }
}
