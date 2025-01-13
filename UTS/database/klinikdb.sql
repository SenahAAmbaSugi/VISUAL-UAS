-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Jan 13, 2025 at 09:32 AM
-- Server version: 8.0.30
-- PHP Version: 8.2.25

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `klinikdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE `admin` (
  `id_admin` int NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `nama_admin` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`id_admin`, `username`, `password`, `nama_admin`) VALUES
(1, 'admin', 'admin123', 'senah');

-- --------------------------------------------------------

--
-- Table structure for table `dokter`
--

CREATE TABLE `dokter` (
  `id_dokter` int NOT NULL,
  `nama_dokter` varchar(100) NOT NULL,
  `status_dokter` varchar(50) NOT NULL,
  `nomor_telepon` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `dokter`
--

INSERT INTO `dokter` (`id_dokter`, `nama_dokter`, `status_dokter`, `nomor_telepon`) VALUES
(1, 'dr.Lisa', 'Aktif', '08233336412'),
(2, 'dr tika ', 'Aktif', '62821276183'),
(3, 'dr.Sis', 'Aktif', '0823457876545');

-- --------------------------------------------------------

--
-- Table structure for table `jadwal_praktek`
--

CREATE TABLE `jadwal_praktek` (
  `id_jadwal` int NOT NULL,
  `id_dokter` int NOT NULL,
  `hari_tanggal` datetime NOT NULL,
  `jam_mulai` time NOT NULL,
  `jam_selesai` time NOT NULL,
  `status_jadwal` enum('Aktif','Non Aktif') NOT NULL DEFAULT 'Aktif',
  `kuota_pasien` int NOT NULL DEFAULT '20',
  `keterangan` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `jadwal_praktek`
--

INSERT INTO `jadwal_praktek` (`id_jadwal`, `id_dokter`, `hari_tanggal`, `jam_mulai`, `jam_selesai`, `status_jadwal`, `kuota_pasien`, `keterangan`) VALUES
(1, 1, '2025-09-01 00:00:00', '08:00:00', '15:30:00', 'Aktif', 18, 'maaf ya halo'),
(3, 2, '2025-01-09 00:00:00', '08:00:00', '17:00:00', 'Aktif', 28, 'sada'),
(4, 1, '2025-01-11 00:00:00', '10:00:00', '14:00:00', 'Aktif', 11, ''),
(5, 3, '2025-01-13 00:00:00', '09:00:00', '12:00:00', 'Aktif', 29, 'menerima penyakit umum'),
(6, 2, '2025-01-10 00:00:00', '08:00:00', '17:00:00', 'Aktif', 5, 'dfgh');

-- --------------------------------------------------------

--
-- Table structure for table `pasien`
--

CREATE TABLE `pasien` (
  `id_pasien` int NOT NULL,
  `nama_pasien` varchar(100) NOT NULL,
  `nomor_kartu` varchar(20) NOT NULL,
  `alamat` text,
  `umur` int DEFAULT NULL,
  `jenis_kelamin` enum('Laki-laki','Perempuan') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `tanggal_lahir` date DEFAULT NULL,
  `no_telepon` varchar(15) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `pasien`
--

INSERT INTO `pasien` (`id_pasien`, `nama_pasien`, `nomor_kartu`, `alamat`, `umur`, `jenis_kelamin`, `tanggal_lahir`, `no_telepon`, `email`) VALUES
(1, 'senah', '12357677', 'toraja', 20, 'Laki-laki', '2025-01-09', '08212341242', ''),
(2, 'Eva', '23456432', 'Mars', 12, 'Perempuan', '2023-10-17', '0823456788765', 'eva@gmail.com'),
(3, 'Sutarni', '1234567876543', 'Tlanakan', 20, 'Perempuan', '2024-08-12', '0823456788765', 'sutarni@gmail.com');

-- --------------------------------------------------------

--
-- Table structure for table `pembayaran`
--

CREATE TABLE `pembayaran` (
  `id_pembayaran` int NOT NULL,
  `no_pembayaran` varchar(50) NOT NULL,
  `id_rekam_medis` int NOT NULL,
  `id_pasien` int NOT NULL,
  `tanggal_pembayaran` date NOT NULL,
  `biaya_konsultasi` decimal(10,2) NOT NULL,
  `biaya_tindakan` decimal(10,2) NOT NULL,
  `total_biaya` decimal(10,2) NOT NULL,
  `status_pembayaran` varchar(20) NOT NULL,
  `metode_pembayaran` enum('Cash','Debit','Transfer') NOT NULL DEFAULT 'Cash'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `pembayaran`
--

INSERT INTO `pembayaran` (`id_pembayaran`, `no_pembayaran`, `id_rekam_medis`, `id_pasien`, `tanggal_pembayaran`, `biaya_konsultasi`, `biaya_tindakan`, `total_biaya`, `status_pembayaran`, `metode_pembayaran`) VALUES
(1, 'PYM202501090001', 2, 1, '2025-01-09', '80000.00', '90902.00', '170902.00', 'Lunas', 'Cash'),
(2, 'PYM202501090002', 3, 3, '2025-01-09', '50000.00', '20000.00', '70000.00', 'Lunas', 'Cash');

-- --------------------------------------------------------

--
-- Table structure for table `pendaftaran`
--

CREATE TABLE `pendaftaran` (
  `id_pendaftaran` int NOT NULL,
  `id_pasien` int NOT NULL,
  `id_jadwal` int NOT NULL,
  `tanggal_pendaftaran` date NOT NULL,
  `nomor_antrian` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `pendaftaran`
--

INSERT INTO `pendaftaran` (`id_pendaftaran`, `id_pasien`, `id_jadwal`, `tanggal_pendaftaran`, `nomor_antrian`) VALUES
(2, 1, 1, '2025-01-09', 1),
(3, 1, 3, '2025-01-09', 1),
(4, 2, 4, '2025-01-09', 1),
(5, 2, 1, '2025-01-09', 2),
(6, 3, 5, '2025-01-09', 1);

-- --------------------------------------------------------

--
-- Table structure for table `rekam_medis`
--

CREATE TABLE `rekam_medis` (
  `id_rekam_medis` int NOT NULL,
  `id_pendaftaran` int NOT NULL,
  `id_dokter` int NOT NULL,
  `keluhan` text NOT NULL,
  `diagnosa` text NOT NULL,
  `resep` text,
  `tindakan` text,
  `tanggal_rekam` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `rekam_medis`
--

INSERT INTO `rekam_medis` (`id_rekam_medis`, `id_pendaftaran`, `id_dokter`, `keluhan`, `diagnosa`, `resep`, `tindakan`, `tanggal_rekam`) VALUES
(2, 2, 1, 'sdf', 'd', 'wew', 'd', '2025-01-09'),
(3, 6, 3, 'demam, flu', 'sakit cuaca', 'pct\r\namxl\r\nmtyhl', 'diberikn obat', '2025-01-09');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`id_admin`),
  ADD UNIQUE KEY `username` (`username`);

--
-- Indexes for table `dokter`
--
ALTER TABLE `dokter`
  ADD PRIMARY KEY (`id_dokter`);

--
-- Indexes for table `jadwal_praktek`
--
ALTER TABLE `jadwal_praktek`
  ADD PRIMARY KEY (`id_jadwal`),
  ADD KEY `id_dokter` (`id_dokter`);

--
-- Indexes for table `pasien`
--
ALTER TABLE `pasien`
  ADD PRIMARY KEY (`id_pasien`),
  ADD UNIQUE KEY `nomor_kartu` (`nomor_kartu`);

--
-- Indexes for table `pembayaran`
--
ALTER TABLE `pembayaran`
  ADD PRIMARY KEY (`id_pembayaran`),
  ADD UNIQUE KEY `no_pembayaran` (`no_pembayaran`),
  ADD KEY `id_rekam_medis` (`id_rekam_medis`),
  ADD KEY `id_pasien` (`id_pasien`);

--
-- Indexes for table `pendaftaran`
--
ALTER TABLE `pendaftaran`
  ADD PRIMARY KEY (`id_pendaftaran`),
  ADD KEY `id_pasien` (`id_pasien`),
  ADD KEY `id_jadwal` (`id_jadwal`);

--
-- Indexes for table `rekam_medis`
--
ALTER TABLE `rekam_medis`
  ADD PRIMARY KEY (`id_rekam_medis`),
  ADD KEY `id_pendaftaran` (`id_pendaftaran`),
  ADD KEY `id_dokter` (`id_dokter`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admin`
--
ALTER TABLE `admin`
  MODIFY `id_admin` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `dokter`
--
ALTER TABLE `dokter`
  MODIFY `id_dokter` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `jadwal_praktek`
--
ALTER TABLE `jadwal_praktek`
  MODIFY `id_jadwal` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pasien`
--
ALTER TABLE `pasien`
  MODIFY `id_pasien` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `pembayaran`
--
ALTER TABLE `pembayaran`
  MODIFY `id_pembayaran` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `pendaftaran`
--
ALTER TABLE `pendaftaran`
  MODIFY `id_pendaftaran` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `rekam_medis`
--
ALTER TABLE `rekam_medis`
  MODIFY `id_rekam_medis` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `jadwal_praktek`
--
ALTER TABLE `jadwal_praktek`
  ADD CONSTRAINT `jadwal_praktek_ibfk_1` FOREIGN KEY (`id_dokter`) REFERENCES `dokter` (`id_dokter`);

--
-- Constraints for table `pembayaran`
--
ALTER TABLE `pembayaran`
  ADD CONSTRAINT `pembayaran_ibfk_1` FOREIGN KEY (`id_rekam_medis`) REFERENCES `rekam_medis` (`id_rekam_medis`),
  ADD CONSTRAINT `pembayaran_ibfk_2` FOREIGN KEY (`id_pasien`) REFERENCES `pasien` (`id_pasien`);

--
-- Constraints for table `pendaftaran`
--
ALTER TABLE `pendaftaran`
  ADD CONSTRAINT `pendaftaran_ibfk_1` FOREIGN KEY (`id_pasien`) REFERENCES `pasien` (`id_pasien`),
  ADD CONSTRAINT `pendaftaran_ibfk_2` FOREIGN KEY (`id_jadwal`) REFERENCES `jadwal_praktek` (`id_jadwal`);

--
-- Constraints for table `rekam_medis`
--
ALTER TABLE `rekam_medis`
  ADD CONSTRAINT `rekam_medis_ibfk_1` FOREIGN KEY (`id_pendaftaran`) REFERENCES `pendaftaran` (`id_pendaftaran`),
  ADD CONSTRAINT `rekam_medis_ibfk_2` FOREIGN KEY (`id_dokter`) REFERENCES `dokter` (`id_dokter`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
