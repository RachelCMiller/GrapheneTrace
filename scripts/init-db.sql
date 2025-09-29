-- GrapheneTrace Database Initialization Script
-- Creates tables for pressure monitoring data

-- Enable UUID extension
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Pressure Data Table
CREATE TABLE IF NOT EXISTS pressure_data (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    sensor_id VARCHAR(50) NOT NULL,
    timestamp TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    pressure_matrix BYTEA NOT NULL,  -- Serialized pressure map data
    peak_pressure SMALLINT NOT NULL CHECK (peak_pressure >= 0 AND peak_pressure <= 255),
    contact_area_percentage DECIMAL(5,2) NOT NULL CHECK (contact_area_percentage >= 0 AND contact_area_percentage <= 100),
    alert_status VARCHAR(20) NOT NULL DEFAULT 'NORMAL',
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Sessions Table
CREATE TABLE IF NOT EXISTS monitoring_sessions (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    session_name VARCHAR(100) NOT NULL,
    start_time TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    end_time TIMESTAMP WITH TIME ZONE,
    sensor_id VARCHAR(50) NOT NULL,
    patient_id VARCHAR(50),
    notes TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Alerts Table
CREATE TABLE IF NOT EXISTS pressure_alerts (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    session_id UUID REFERENCES monitoring_sessions(id) ON DELETE CASCADE,
    pressure_data_id UUID REFERENCES pressure_data(id) ON DELETE CASCADE,
    alert_type VARCHAR(50) NOT NULL DEFAULT 'HIGH_PRESSURE',
    threshold_value SMALLINT NOT NULL,
    actual_value SMALLINT NOT NULL,
    timestamp TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    acknowledged BOOLEAN DEFAULT FALSE,
    acknowledged_by VARCHAR(100),
    acknowledged_at TIMESTAMP WITH TIME ZONE
);

-- Create indexes for performance
CREATE INDEX IF NOT EXISTS idx_pressure_data_timestamp ON pressure_data(timestamp);
CREATE INDEX IF NOT EXISTS idx_pressure_data_sensor_id ON pressure_data(sensor_id);
CREATE INDEX IF NOT EXISTS idx_monitoring_sessions_start_time ON monitoring_sessions(start_time);
CREATE INDEX IF NOT EXISTS idx_pressure_alerts_timestamp ON pressure_alerts(timestamp);
CREATE INDEX IF NOT EXISTS idx_pressure_alerts_acknowledged ON pressure_alerts(acknowledged);

-- Insert sample data for testing
INSERT INTO monitoring_sessions (session_name, sensor_id, patient_id, notes)
VALUES ('Demo Session', 'DEMO_SENSOR_01', 'PATIENT_001', 'Initial demo session for GrapheneTrace system');